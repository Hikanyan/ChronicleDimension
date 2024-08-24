using UniRx;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public interface ICriVolume
    {
        IReactiveProperty<float> Volume { get; }
        void SetVolume(float volume);
    }
}