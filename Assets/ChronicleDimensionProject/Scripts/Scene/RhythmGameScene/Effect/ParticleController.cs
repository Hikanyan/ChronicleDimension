using System.Collections.Generic;
using UnityEngine;

namespace ChronicleDimensionProject.RhythmGameScene
{
    public class ParticleController:MonoBehaviour
    {
        ParticleSystem[] _particles;
        void Start()
        {
            _particles = GetComponentsInChildren<ParticleSystem>();
        }

        public void Play()
        {
            foreach(ParticleSystem particle in _particles)
            {
                particle.Play();
            }
        }

        public void Stop()
        {
            foreach (ParticleSystem particle in _particles)
            {
                particle.Stop();
            }
        }
    }
}