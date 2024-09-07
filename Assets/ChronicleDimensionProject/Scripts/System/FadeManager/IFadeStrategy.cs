using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChargeCatProject.System
{
    public interface IFadeStrategy
    {
        UniTask FadeOut(Material fadeMaterial);
        UniTask FadeIn(Material fadeMaterial);
    }
}