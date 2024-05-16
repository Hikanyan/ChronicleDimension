using System.Collections;
using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using UnityEngine;

namespace ChronicleDimensionProject.Common.Test
{
    public class CriPlay : MonoBehaviour
    {
        [SerializeField] private string cueNameBgm;
        [SerializeField] private string cueNameSe;

        private CriAudioManager _criAudioManager;

        void Start()
        {
            _criAudioManager = CriAudioManager.Instance;
        }

        void Update()
        {
            // BGMの再生と停止
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("BGMを再生");
                _criAudioManager.PlayBGM(cueNameBgm);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("BGMを停止");
                _criAudioManager.StopBGM();
            }

            // SEの再生と停止
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("SEを再生");
                _criAudioManager.PlaySE(cueNameSe);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("SEを停止");
                _criAudioManager.StopSE(0); // 再生されたSEのインデックスを指定
            }

            // ボイスの再生と停止
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("ボイスを再生");
                _criAudioManager.PlayVoice(CriAudioManager.CueSheet.Voice, "SomeVoiceCueName");
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("ボイスを停止");
                _criAudioManager.StopVoice(0); // 再生されたボイスのインデックスを指定
            }

            // マスターボリュームの調整
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _criAudioManager.MasterVolume += 0.1f;
                Debug.Log($"マスターボリュームを上げる: {_criAudioManager.MasterVolume}");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _criAudioManager.MasterVolume -= 0.1f;
                Debug.Log($"マスターボリュームを下げる: {_criAudioManager.MasterVolume}");
            }
        }
    }
}