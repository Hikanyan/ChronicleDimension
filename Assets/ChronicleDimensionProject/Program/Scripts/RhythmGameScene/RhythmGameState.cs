using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.Common.UI;
using State = StateMachine<ChronicleDimensionProject.Boot.GameManager>.State;

namespace ChronicleDimensionProject.Program.Scripts.RhythmGameScene
{
    public class RhythmGameState : State
    {
        protected override async void OnEnter(State prevState)
        {
            await LoadingScene.Instance.LoadNextScene("RhythmGameScene");
        }

        protected override void OnUpdate()
        {
            
        }

        protected override void OnExit(State nextState)
        {
            
        }
    }
}