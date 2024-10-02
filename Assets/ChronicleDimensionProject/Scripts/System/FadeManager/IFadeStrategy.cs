using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.Common
{
    public interface IFadeStrategy
    {
        UniTask FadeOut(Material fadeMaterial);
        UniTask FadeIn(Material fadeMaterial);
    }
}