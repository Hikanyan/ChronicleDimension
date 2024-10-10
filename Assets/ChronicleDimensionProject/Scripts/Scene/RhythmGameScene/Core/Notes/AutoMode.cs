using ChronicleDimensionProject.Common;
using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public class AutoMode:Singleton<AutoMode>
    {
       [HideInInspector] public bool _autoMode = false;   //オートモード初期値false


        public void OnClickAutoModeButton()
        {
            switch (_autoMode)
            {
                case false:
                    _autoMode = true;
                    Debug.Log($"オートモード{_autoMode}");
                    break;
                case true:
                    _autoMode = false;
                    Debug.Log($"オートモード{_autoMode}");
                    break;
            }
        }
    }
}