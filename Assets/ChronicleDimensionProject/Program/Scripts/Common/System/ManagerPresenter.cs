using UnityEditor.Experimental.GraphView;
using VContainer.Unity;

namespace ChronicleDimensionProject
{
    public class ManagerPresenter : IStartable, ITickable
    {
        private readonly bool _isDebugMode;

        public ManagerPresenter(
            bool isDebugMode
        )
        {
            _isDebugMode = isDebugMode;
        }

        public void Start()
        {
        }

        public void Tick()
        {
        }
    }
}