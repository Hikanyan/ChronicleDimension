using ChronicleDimention.CueSheet_BGM;
using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Audio;
using UniRx;
using UnityEngine;

namespace HikanyanLaboratory.Script.Audio
{
    public class TestCri : MonoBehaviour
    {
        private void Start()
        {
            // 初期状態でBGMを再生
            CriAudioManager.Instance.Play(CriAudioType.CueSheet_BGM, "Meteor Shower");

            // キー入力を監視
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.P))
                .Subscribe(_ => PlayBGM());

            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.O))
                .Subscribe(_ => PlaySE());

            _ = Task();
        }

        private void PlayBGM()
        {
            CriAudioManager.Instance.Play(CriAudioType.CueSheet_BGM, Cue.MeteorShower);
        }

        private void PlaySE()
        {
            CriAudioManager.Instance.Play(CriAudioType.CueSheet_SE, "Perfect");
        }

        async UniTask Task()
        {
            await UniTask.Delay(1000);
            // 1秒後にSEを再生
            for (int i = 0; i < 50; i++)
            {
                CriAudioManager.Instance.Play(CriAudioType.CueSheet_SE, "Perfect");
            }
        }
    }
}