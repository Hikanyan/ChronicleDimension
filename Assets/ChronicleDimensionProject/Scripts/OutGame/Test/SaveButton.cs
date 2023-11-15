using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.Scripts.OutGame.Test
{
    /// <summary>
    /// SaveButtonクラスはセーブやロードを行うボタンのクラスです。
    /// Test用で使用されます。
    /// </summary>
    public class SaveButton : MonoBehaviour
    {
        [SerializeField] Button saveButton;
        [SerializeField] Button loadButton;
        [SerializeField] Button deleteButton;
        [SerializeField] Text text;
    
        void Start()
        {
            // ボタンが押された時の処理を登録する
            //AddListener()はボタンが押された時の処理を登録するメソッドです。
            saveButton.onClick.AddListener(OnClickSave);
            // ボタンが押された時の処理を登録する
            loadButton.onClick.AddListener(OnClickLoad);
            // ボタンが押された時の処理を登録する
            deleteButton.onClick.AddListener(OnClickDelete);
        }

        /// <summary>
        /// OnClickSave()はセーブボタンが押された時の処理です。
        /// </summary>
        public void OnClickSave()
        {
            // SaveManager.Instance.SaveData.PlayerName = "test";
            // SaveManager.Instance.SaveGame();
            Debug.Log("Saveしました。");
        }

        /// <summary>
        /// OnClickLoad()はロードボタンが押された時の処理です。
        /// </summary>
        public void OnClickLoad()
        {
            // SaveData setting = SaveManager.SaveSettings<SaveData>();
            // Debug.Log($"Loadしました。{SaveManager.Instance.SaveData.PlayerName}");
        }
        /// <summary>
        /// OnClickDelete()はDeleteボタンが押された時の処理です。
        /// </summary>
        public void OnClickDelete()
        {
        
        }
    
    
        //デバッグ用のテキストを更新する
        private void Update()
        {
        
        }
    }
}