using ChronicleDimensionProject.Boot;
using State = StateMachine<ChronicleDimensionProject.Boot.GameManager>.State;

namespace ChronicleDimensionProject.Title
{
    public class TitleState : State
    {
        protected override async void OnEnter(State prevState)
        {
            await SceneController.Instance.LoadScene("TitleScene");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnExit(State nextState)
        {
        }
    }
}