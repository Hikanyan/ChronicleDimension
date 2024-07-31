using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public abstract class Node : MonoBehaviour
    {
        public virtual UniTask OnInitialize() => UniTask.CompletedTask;
        public virtual UniTask OnOpenIn() => UniTask.CompletedTask;
        public virtual UniTask OnCloseIn() => UniTask.CompletedTask;
        public virtual UniTask OnOpenOut() => UniTask.CompletedTask;
        public virtual UniTask OnCloseOut() => UniTask.CompletedTask;
    }
}