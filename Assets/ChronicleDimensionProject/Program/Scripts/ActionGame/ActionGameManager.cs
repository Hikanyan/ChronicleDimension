using ChronicleDimensionProject.Program.Scripts.Boot.Interface;
using ChronicleDimensionProject.Common;

namespace ChronicleDimensionProject.InGame.ActionGame
{
    public class ActionGameManager : AbstractSingleton<ActionGameManager>, IGameModeManager
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