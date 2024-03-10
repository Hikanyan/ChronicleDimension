using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.Common.UI;
using State = StateMachine<ChronicleDimensionProject.Boot.GameManager>.State;

namespace ChronicleDimensionProject.GameSelectScene
{
    public class GameSelectState : State
    {
        protected override async void OnEnter(State prevState)
        {
            await LoadingScene.Instance.LoadNextScene("GameSelectScene");
            //CriAudioManager.Instance.PlayBGM(CriAudioManager.CueSheet.Bgm, "Title");
            //CriAudioManager.Instance.PlayBGM(CriAudioManager.CueSheet.Voice, "");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnExit(State nextState)
        {
        }
    }
}