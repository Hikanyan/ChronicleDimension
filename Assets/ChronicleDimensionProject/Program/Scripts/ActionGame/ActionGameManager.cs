using ChronicleDimensionProject.Program.Scripts.Boot.Interface;
using ChronicleDimensionProject.Common;

namespace ChronicleDimensionProject.InGame.ActionGame
{
    public class ActionGameManager : AbstractSingletonMonoBehaviour<ActionGameManager>, IGameModeManager
    {
        public void Activate()
        {
        }

        public void Deactivate()
        {
            throw new System.NotImplementedException();
        }
    }
}