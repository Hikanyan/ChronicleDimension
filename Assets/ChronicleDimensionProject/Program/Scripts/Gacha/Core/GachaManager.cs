using ChronicleDimensionProject.Common;

namespace ChronicleDimensionProject.Program.Scripts.Gacha.Core
{
    public class GachaManager: AbstractSingletonMonoBehaviour<GachaManager>
    {
        protected override bool UseDontDestroyOnLoad => false;
    }
}