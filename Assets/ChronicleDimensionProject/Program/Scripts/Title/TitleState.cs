using ChronicleDimensionProject.Boot;
using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.Common.UI;
using State = StateMachine<ChronicleDimensionProject.Boot.GameManager>.State;

namespace ChronicleDimensionProject.Title
{
    public class TitleState : State
    {
        protected override void OnEnter(State prevState)
        {
            SceneController.Instance.LoadScene("TitleScene");
            //CriAudioManager.Instance.PlayBGM(CriAudioManager.CueSheet.Bgm, "Title");
            CriAudioManager.Instance.PlayBGM(CriAudioManager.CueSheet.Voice, "AIVoiceクロニカルディメンション");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnExit(State nextState)
        {
        }
    }
}