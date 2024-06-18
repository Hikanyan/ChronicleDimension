using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ChronicleDimensionProject
{
    public class ManagerLifetimeScope : LifetimeScope
    {
        [Tooltip("デバック用")] [SerializeField] private bool _isDebugMode;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<ManagerPresenter>().WithParameter("isDebugMode", _isDebugMode);
        }
    }
}

