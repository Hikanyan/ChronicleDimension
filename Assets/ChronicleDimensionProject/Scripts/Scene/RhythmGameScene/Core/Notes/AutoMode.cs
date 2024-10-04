using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public class AutoMode:MonoBehaviour
    {
        public static AutoMode Instance;                      //オートモードのBoolを引き継ぐためのシングルトン
        [HideInInspector] public bool _autoMode = false;   //オートモード初期値false

        private void Awake()                                //シングルトンのお約束
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this.gameObject);             //このゲームオブジェクトが壊され無くなった
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public void OnClickAutoModeButton()
        {
            if (_autoMode == false)
            {
                _autoMode = true;
                Debug.Log($"オートモード{_autoMode}");
            }
            else if (_autoMode == true)
            {
                _autoMode = false;
                Debug.Log($"オートモード{_autoMode}");
            }
        }
    }
}