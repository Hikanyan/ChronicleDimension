using VContainer;
using VContainer.Unity;

namespace ChronicleDimensionProject
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            
            //Register<TInterface, TImplement> と As<TInterface> でインターフェースと実装クラスを登録する
        }
    }
}