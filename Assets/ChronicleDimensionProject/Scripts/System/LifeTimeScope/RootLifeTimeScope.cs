using ChronicleDimensionProject.Network;
using HikanyanLaboratory.Audio;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ChronicleDimensionProject.System
{
    /// <summary>
    /// GameManager
    /// SceneManager
    /// LoginManager
    /// AudioManager
    /// </summary>
    //[RuntimeInitializeOnLoadMethod]
    public class RootLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameManager>(Lifetime.Singleton);


            // Fadeの登録
            builder.Register<IFadeStrategy, BasicFadeStrategy>(Lifetime.Singleton);
            builder.Register<FadeManager>(Lifetime.Singleton);
            builder.RegisterEntryPoint<FadeView>();

            // PlayFabの登録
            builder.Register<PlayFabLogin>(Lifetime.Singleton);

            // AudioManagerの登録
            builder.Register<CriAudioManager>(Lifetime.Singleton);
        }
    }
}