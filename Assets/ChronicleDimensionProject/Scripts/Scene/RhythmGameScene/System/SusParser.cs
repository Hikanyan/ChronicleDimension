using System.Collections.Generic;
using System.IO;
using ChronicleDimensionProject.RhythmGame.Notes;

namespace ChronicleDimensionProject.Scripts.Scene.RhythmGameScene.System
{
    public class SusParser
    {
         public TapNotesInput[] TapNotes { get; private set; }
        public HoldNotesInput[] HoldNotes { get; private set; }

        public void ParseSusFile(string filePath)
        {
            List<TapNotesInput> tapNotesList = new();
            List<HoldNotesInput> holdNotesList = new();

            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (line.StartsWith("#"))
                {
                    // メタデータ行の処理（必要なら処理を追加）
                    continue;
                }

                // 小節のノーツ情報をパース
                if (!line.Contains(":")) continue;
                string[] parts = line.Split(':');
                string measureInfo = parts[0]; // 小節とノーツ種類の情報
                string noteData = parts[1];    // ノーツデータ

                // ノーツの種類に応じてパース
                if (measureInfo.EndsWith("1")) // Tapノーツ
                {
                    tapNotesList.Add(ParseTapNotes(measureInfo, noteData));
                }
                else if (measureInfo.EndsWith("2")) // Holdノーツ
                {
                    holdNotesList.Add(ParseHoldNotes(measureInfo, noteData));
                }
            }

            TapNotes = tapNotesList.ToArray();
            HoldNotes = holdNotesList.ToArray();
        }

        private TapNotesInput ParseTapNotes(string measureInfo, string noteData)
        {
            // ノーツデータのパース処理（適宜修正が必要）
            return new TapNotesInput
            {
                _type = 1, // 仮のノーツタイプ（SUSの仕様に合わせて適宜修正）
                _time = CalculateTimeFromMeasure(measureInfo), // 小節情報から時間を計算
                _block = CalculateBlockFromData(noteData)      // ノーツのレーンを計算
            };
        }

        private HoldNotesInput ParseHoldNotes(string measureInfo, string noteData)
        {
            // ノーツデータのパース処理（適宜修正が必要）
            return new HoldNotesInput
            {
                _type = 2, // 仮のノーツタイプ（SUSの仕様に合わせて適宜修正）
                _time = new float[] { CalculateTimeFromMeasure(measureInfo), CalculateHoldEndTime(noteData) },
                _block = CalculateBlockFromData(noteData)
            };
        }

        private float CalculateTimeFromMeasure(string measureInfo)
        {
            // SUS形式の小節情報から時間を計算するロジック
            // 仮の計算式、実際の小節情報に合わせて修正
            return float.Parse(measureInfo.Substring(0, 3)) * 4.0f;
        }

        private int CalculateBlockFromData(string noteData)
        {
            // ノーツデータからレーン（ブロック）を計算
            // 仮の計算式、実際のデータ形式に合わせて修正
            return int.Parse(noteData.Substring(0, 1));
        }

        private float CalculateHoldEndTime(string noteData)
        {
            // Holdノーツの終了時間を計算
            // 仮の計算式、実際のデータ形式に合わせて修正
            return float.Parse(noteData.Substring(1, 3)) * 4.0f;
        }
    }
}