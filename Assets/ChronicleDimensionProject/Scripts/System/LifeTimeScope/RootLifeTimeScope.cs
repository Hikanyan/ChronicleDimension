using ChargeCatProject.System;
using Photon.Pun.Demo.PunBasics;
using VContainer;
using VContainer.Unity;

namespace ChronicleDimensionProject.System
{
    public class RootLifeTimeScope : LifetimeScope
    {
        // LifetimeScopeが開始されるときに呼び出されるメソッド
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameManager>(Lifetime.Singleton);
            
            
            // FadeManagerの登録
            builder.Register<IFadeStrategy, BasicFadeStrategy>(Lifetime.Singleton);
            builder.Register<FadeManager>(Lifetime.Singleton);
            builder.RegisterEntryPoint<FadeView>(); // MonoBehaviourの場合はEntryPointで登録
            
            
        }
    }
}