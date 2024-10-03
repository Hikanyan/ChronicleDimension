using System;
using System.Collections.Generic;

namespace ChronicleDimensionProject.PlayerData
{
    public class PlayerSettings
    {
        public RhythmGamePlayerData RhythmGamePlayerData { get; set; } = new RhythmGamePlayerData();
        public VolumeData Volume { get; set; } = new VolumeData();
        public bool ShowDebugInfo { get; set; } = false; // プロパティに変更
    }

    public class VolumeData
    {
        public float MusicVolume { get; set; } = 1.0f;
        public float MasterVolume { get; set; } = 1.0f;
        public float BGMVolume { get; set; } = 1.0f;
        public float SeVolume { get; set; } = 1.0f;
        public float VoiceVolume { get; set; } = 1.0f;
    }

    public class RhythmGamePlayerData
    {
        public float NotesSpeed { get; set; } = 10.0f;
        public float JudgeOffset { get; set; } = 0.0f;
    }

    public class StoryProgressData
    {
        // 12星座分のデータ
        public int Aries { get; set; } = 0; // おひつじ座
        public int Taurus { get; set; } = 0; // おうし座
        public int Gemini { get; set; } = 0; // ふたご座
        public int Cancer { get; set; } = 0; // かに座
        public int Leo { get; set; } = 0; // しし座
        public int Virgo { get; set; } = 0; // おとめ座
        public int Libra { get; set; } = 0; // てんびん座
        public int Scorpio { get; set; } = 0; // さそり座
        public int Sagittarius { get; set; } = 0; // いて座
        public int Capricorn { get; set; } = 0; // やぎ座
        public int Aquarius { get; set; } = 0; // みずがめ座
        public int Pisces { get; set; } = 0; // うお座

        // 進捗の合計を返すメソッド (必要に応じて)
        public int TotalProgress()
        {
            return Aries + Taurus + Gemini + Cancer + Leo + Virgo +
                   Libra + Scorpio + Sagittarius + Capricorn + Aquarius + Pisces;
        }
    }

    public class PlayerID
    {
        public string PlayerName { get; set; }

        // UserID は16桁の文字列
        private string _userID;

        public string UserID
        {
            get => _userID;
            set
            {
                if (value.Length == 16)
                {
                    _userID = value;
                }
                else
                {
                    throw new ArgumentException("UserID must be a 16-digit string.");
                }
            }
        }
    }

    // デイリーミッションのデータクラスを追加
    public class DailyMissionData
    {
        public string MissionName { get; set; } // ミッション名
        public string Description { get; set; } // ミッションの説明
        public bool IsCompleted { get; set; } = false; // 完了フラグ
        public int Progress { get; set; } = 0; // 進捗状況 (例: 0/10)
        public int Target { get; set; } // 目標値 (例: 10)

        public DateTime LastResetTime { get; set; } // 最後のリセット時間

        // 報酬に関するデータ (例: ゲーム内通貨、アイテム等)
        public RewardData Reward { get; set; } = new RewardData();
    }

    public class RewardData
    {
        public int Currency { get; set; } = 0; // 通貨報酬
        public List<ItemData> Items { get; set; } = new List<ItemData>(); // アイテムのリスト
    }

    // インベントリーデータを追加
    public class InventoryData
    {
        public int Gems { get; set; } = 0; // ジェムの数
        public int Coins { get; set; } = 0; // コインの数
        public int Stamina { get; set; } = 100; // スタミナ (デフォルト100)

        // アイテムのリスト（例えば、インベントリ内のアイテム名や個数など）
        public List<ItemData> Items { get; set; } = new List<ItemData>();
    }

    // インベントリ内のアイテムデータ
    public class ItemData
    {
        public string ItemName { get; set; } // アイテム名
        public int Quantity { get; set; } // アイテムの個数
    }
}