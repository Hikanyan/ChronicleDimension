using ChronicleDimensionProject.Common.UI;
using State = StateMachine<ChronicleDimensionProject.Boot.GameManager>.State;

namespace ChronicleDimensionProject.Title
{
    public class TitleState : State
    {
        protected override async void OnEnter(State prevState)
        {
            await LoadingScene.Instance.LoadNextScene("TitleScene");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnExit(State nextState)
        {
        }
    }
}