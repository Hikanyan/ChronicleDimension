using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ChronicleDimensionProject.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace ChronicleDimensionProject.RhythmGameScene
{
    // SUSファイルのメタデータと譜面データを保持するクラス
    [Serializable]
    public class SusData
    {
        public string Title; // 曲名
        public string Subtitle; // サブタイトル
        public string Artist; // アーティスト名
        public string Genre; // ジャンル
        public string Designer; // 譜面デザイナー
        public MusicDifficultyLevel Difficulty; // 難易度
        public int PlayLevel; // プレイレベル
        public string SongId; // 曲ID
        public string Wave; // 音声ファイル名
        public float WaveOffset; // 音声ファイルオフセット
        public string Jacket; // ジャケット画像ファイル名
        public string Request; // リクエスト(ticks_per_beat 480)
        public string Background; // 背景画像ファイル名
        public string Movie; // 動画ファイル名
        public float MovieOffset; // 動画ファイルオフセット
        public float BaseBPM; // 基本BPM


        public List<NoteData> Notes = new List<NoteData>(); // ノートデータのリスト
        public List<BPMApplication> BPMApplications = new List<BPMApplication>(); // BPM適用情報
        public Dictionary<string, float> BPMDefinitions = new Dictionary<string, float>(); // BPM定義

        public Dictionary<string, List<SpeedChange>>
            SpeedChanges = new Dictionary<string, List<SpeedChange>>(); // スピード変更
    }

    [Serializable]
    public class NoteData
    {
        public int Measure; // 小節番号
        public int Lane; // レーン番号
        public string Type; // ノートタイプ（例: "Tap", "Hold", "Slide", "Directional"）
        public float StartTime; // 開始時間
        public float EndTime; // 終了時間（ホールドとスライド用）
        public List<float> ControlPoints; // 制御点（スライド用）、時間オフセットのリストでベジエ制御点を表す
    }

    [Serializable]
    public class SpeedChange
    {
        public int Measure; // 小節番号
        public int Tick; // ティック
        public float Speed; // スピード
    }

    [Serializable]
    public class BPMApplication
    {
        public int Measure; // 適用開始小節
        public float BpmValue; // 適用されるBPMのID
    }


    public class SusParser
    {
        public SusData ParseSusFile(string filePath)
        {
            var susData = new SusData();
            string[] fileLines = File.ReadAllLines(filePath);

            foreach (string line in fileLines)
            {
                if (line.StartsWith("#"))
                {
                    ParseLine(line, ref susData);
                }
            }


            DebugLogSusData(susData);

            return susData;
        }


        private void ParseLine(string line, ref SusData susData)
        {
            if (line.StartsWith("#TITLE "))
            {
                // "#TITLE "の後のダブルクォーテーションで囲まれたテキストを抽出
                susData.Title = ExtractInfo(line);
            }
            else if (line.StartsWith("#ARTIST "))
            {
                susData.Artist = ExtractInfo(line);
            }
            else if (line.StartsWith("#DESIGNER "))
            {
                susData.Designer = ExtractInfo(line);
            }
            else if (line.StartsWith("#DIFFICULTY "))
            {
                // 難易度はダブルクォーテーションで囲まれたテキストではないので、そのまま抽出
                susData.Difficulty = (MusicDifficultyLevel)Enum.Parse(typeof(Notes.NotesType), line.Substring(12));
            }
            else if (line.StartsWith("#PLAYLEVEL "))
            {
                susData.PlayLevel = int.Parse(line.Substring(11));
            }
            else if (line.StartsWith("#SONGID "))
            {
                susData.SongId = ExtractInfo(line);
            }
            else if (line.StartsWith("#WAVE "))
            {
                susData.Wave = ExtractInfo(line);

                // もし、.wavや.mp3などの拡張子が含まれている場合は、それを取り除く
                // 抽出したテキストから、.wavや.mp3などの拡張子が含まれている場合は、それを取り除く
                if (susData.Wave.Contains("."))
                {
                    susData.Wave = susData.Wave.Substring(0, susData.Wave.LastIndexOf('.'));
                }
            }
            else if (line.StartsWith("#WAVEOFFSET "))
            {
                susData.WaveOffset = float.Parse(line.Substring(12));
            }
            else if (line.StartsWith("#JACKET "))
            {
                susData.Jacket = ExtractInfo(line);
            }

            else if (line.StartsWith("#REQUEST "))
            {
                susData.Request = ExtractInfo(line);
            }

            else if (line.StartsWith("#SUBTITLE "))
            {
                susData.Subtitle = ExtractInfo(line);
            }
            else if (line.StartsWith("#GENRE "))
            {
                susData.Genre = ExtractInfo(line);
            }
            else if (line.StartsWith("#BACKGROUND "))
            {
                susData.Background = ExtractInfo(line);
            }
            else if (line.StartsWith("#MOVIE "))
            {
                susData.Movie = ExtractInfo(line);
            }
            else if (line.StartsWith("#MOVIEOFFSET "))
            {
                susData.MovieOffset = float.Parse(line.Substring(13));
            }
            else if (line.StartsWith("#BASEBPM "))
            {
                susData.BaseBPM = float.Parse(line.Substring(10));
            }

            // BPM定義行の解析
            else if (Regex.IsMatch(line, @"^#BPM\d{2}:"))
            {
                var match = Regex.Match(line, @"^#BPM(\d{2}):\s*(\d+(\.\d+)?)$");
                if (!match.Success) return;
                var bpmId = match.Groups[1].Value; // BPM変更ID"xx" を取得
                var bpmValue = float.Parse(match.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture); // BPM値を取得
                susData.BPMDefinitions[bpmId] = bpmValue; // BPM変更IDをキーとしてBPM値を格納
            }
            else if (Regex.IsMatch(line, @"^#\d{3}08:"))
            {
                var measurePattern = @"^#(\d{3})08:\s*([0-9A-F]+)$"; // BPM適用行のパターンを定義
                var match = Regex.Match(line, measurePattern);
                if (!match.Success) return;
                var measure = int.Parse(match.Groups[1].Value); // 小節番号を解析
                var bpmIds = match.Groups[2].Value; // BPM ID群を取得
                foreach (var bpmValue in from entry in susData.BPMDefinitions where bpmIds.Contains(entry.Key) select entry.Value)
                {
                    susData.BPMApplications.Add(new BPMApplication { Measure = measure, BpmValue = bpmValue }); // BPM適用情報をリストに追加
                    break; // 最初にマッチしたBPM IDで処理を終了
                }
            }
            // 譜面データ行の解析
            else if (Regex.IsMatch(line, @"^#\d{3}\d{2}:"))
            {
                // 譜面データ行の解析
                ParseNoteDataLine(line, ref susData);
            }
        }

        /// <summary>
        /// ダブルクォーテーションで囲まれた情報を抽出するヘルパーメソッド
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string ExtractInfo(string line)
        {
            int startIndex = line.IndexOf('"') + 1;
            int endIndex = line.LastIndexOf('"');
            if (startIndex < endIndex)
            {
                return line.Substring(startIndex, endIndex - startIndex);
            }

            return string.Empty; // 抽出できなかった場合は空の文字列を返す
        }

        /// <summary>
        /// 譜面データ行の解析を行うヘルパーメソッド
        /// </summary>
        /// <param name="line"></param>
        /// <param name="susData"></param>
        private void ParseNoteDataLine(string line, ref SusData susData)
        {
            // 譜面データ行のフォーマットは "#mmmxx: data" です。
            // ここで、mmmは小節番号、xxはチャンネル、dataはノーツデータを表します。

            var mmm = line.Substring(0, 7); // "mmm" の部分を取得
            var xx = line.Substring(7, 2); // "xx" の部分を取得
            var data = line.Substring(8); // 実際のノーツデータ部分

            // ノーツデータの解析とSusDataオブジェクトへの格納
            // この部分は、SusDataの構造に応じて適宜調整が必要です。

            // 例: 小節番号とチャンネルに基づいてノーツデータを格納
            // if (!susData.Notes.ContainsKey(header))
            // {
            //     susData.Notes[header] = new List<string>();
            // }
            // susData.Notes[header].Add(data);
        }


        // SusDataオブジェクトを解析した後に、その内容をログに出力するメソッド
        public void DebugLogSusData(SusData susData)
        {
            Debug.Log($"Title: {susData.Title}");
            Debug.Log($"Subtitle: {susData.Subtitle}");
            Debug.Log($"Artist: {susData.Artist}");
            Debug.Log($"Genre: {susData.Genre}");
            Debug.Log($"Designer: {susData.Designer}");
            Debug.Log($"Difficulty: {susData.Difficulty}");
            Debug.Log($"PlayLevel: {susData.PlayLevel}");
            Debug.Log($"SongId: {susData.SongId}");
            Debug.Log($"Wave: {susData.Wave}");
            Debug.Log($"WaveOffset: {susData.WaveOffset}");
            Debug.Log($"Jacket: {susData.Jacket}");
            Debug.Log($"Request: {susData.Request}");
            Debug.Log($"Background: {susData.Background}");
            Debug.Log($"Movie: {susData.Movie}");
            Debug.Log($"MovieOffset: {susData.MovieOffset}");
            Debug.Log($"BaseBPM: {susData.BaseBPM}");
        }

        public SusData ParseSusString(string fileContent)
        {
            var susData = new SusData();
            string[] fileLines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string line in fileLines)
            {
                if (line.StartsWith("#"))
                {
                    ParseLine(line, ref susData);
                }
            }

            return susData;
        }
    }
}






/*
 BPMの定義と適用情報を解析するサンプルコード
 
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Hello
{
    public static void Main()
    {
        // BPM定義とBPM適用情報を格納するための辞書とリスト
        var BPMDefinitions = new Dictionary<string, float>();
        var BPMApplications = new List<Tuple<int, float>>();

        // ユーザーからの最初の入力（BPM定義）を読み取る
        Console.WriteLine("BPM定義を入力してください（例: #BPM01: 160）:");
        var lineBpm = Console.ReadLine();

        // BPM定義行を解析
        var matchBpm = Regex.Match(lineBpm, @"^#BPM(\d{2}):\s*(\d+(\.\d+)?)$");
        if (matchBpm.Success)
        {
            var bpmId = matchBpm.Groups[1].Value;
            var bpmValue = float.Parse(matchBpm.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture);
            BPMDefinitions[bpmId] = bpmValue;
            Console.WriteLine($"BPM定義を追加しました: ID={bpmId}, 値={bpmValue}");
        }
        else
        {
            Console.WriteLine("入力されたBPM定義は無効です。");
            return;
        }

        // ユーザーからの二番目の入力（BPM適用情報）を読み取る
        Console.WriteLine("BPM適用情報を入力してください（例: #00008:01）:");
        var line = Console.ReadLine();

        // BPM適用情報行を解析
        var measurePattern = @"^#(\d{3})08:\s*([0-9A-F]+)$";
        var match = Regex.Match(line, measurePattern);
        if (match.Success)
        {
            var measure = int.Parse(match.Groups[1].Value);
            var bpmIds = match.Groups[2].Value;
            foreach (var bpmIdKey in BPMDefinitions.Keys)
            {
                if (bpmIds.Contains(bpmIdKey))
                {
                    var bpmValue = BPMDefinitions[bpmIdKey];
                    BPMApplications.Add(new Tuple<int, float>(measure, bpmValue));
                    Console.WriteLine($"BPM適用情報を追加しました: 小節={measure}, BPM値={bpmValue}");
                }
            }
        }
        else
        {
            Console.WriteLine("入力されたBPM適用情報は無効です。");
        }
    }
}

 */