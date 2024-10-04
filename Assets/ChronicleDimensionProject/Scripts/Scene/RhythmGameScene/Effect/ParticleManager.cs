using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.RhythmGame.JudgeData;
using TMPro;
using UnityEngine;

namespace ChronicleDimensionProject.RhythmGameScene
{
    public class ParticleManager : Singleton<ParticleManager>
    {
        [Header("JudgeText")] [SerializeField] TextMeshPro _purePerfectText = default;
        [SerializeField] TextMeshPro _perfectText = default;
        [SerializeField] TextMeshPro _greatText = default;
        [SerializeField] TextMeshPro _goodText = default;
        [SerializeField] TextMeshPro _badText = default;
        [SerializeField] TextMeshPro _missText = default;

        [Header("JudgeParticle")] [SerializeField]
        GameObject _purePerfectParticle = default;

        [SerializeField] GameObject _perfectParticle = default;
        [SerializeField] GameObject _greatParticle = default;
        [SerializeField] GameObject _goodParticle = default;
        [SerializeField] GameObject _badParticle = default;
        [SerializeField] GameObject _missParticle = default;

        [Header("divisionary")] [SerializeField]
        GameObject _holdingParticle = default;

        private readonly List<List<ParticleController>> _particles = new(6);
        private readonly List<List<Animation>> _textParticles = new(6);
        private readonly List<ParticleController> _holdParticles = new();

        void Start()
        {
            GameObject[] selectParticle =
            {
                _purePerfectParticle,
                _perfectParticle,
                _greatParticle,
                _goodParticle,
                _badParticle,
                _missParticle
            };

            TextMeshPro[] selectTextParticle =
            {
                _purePerfectText,
                _perfectText,
                _greatText,
                _goodText,
                _badText,
                _missText
            };

            for (int i = 0; i < 4; i++)
            {
                List<ParticleController> tempParticleList = new();
                List<Animation> tempTextParticleList = new();
                for (int l = 0; l < 6; l++)
                {
                    ParticleController tempParticle =
                        Instantiate(selectParticle[l], new(i - 1.5f, 0.55f, 0), selectParticle[l].transform.rotation)
                            .GetComponent<ParticleController>();
                    Animation tempTextParticle = Instantiate(selectTextParticle[l], new(i - 1.5f, 0.55f, 0),
                        selectTextParticle[l].transform.rotation).GetComponent<Animation>();

                    tempParticle.transform.SetParent(gameObject.transform);
                    tempTextParticle.transform.SetParent(gameObject.transform);

                    tempParticleList.Add(tempParticle);
                    tempTextParticleList.Add(tempTextParticle);
                }

                _particles.Add(tempParticleList);
                _textParticles.Add(tempTextParticleList);

                ParticleController tempHoldParticle =
                    Instantiate(_holdingParticle, new(i - 1.5f, 0.55f, 0), _holdingParticle.transform.rotation)
                        .GetComponent<ParticleController>();
                tempHoldParticle.transform.SetParent(gameObject.transform);
                _holdParticles.Add(tempHoldParticle);
            }
        }

        public void HoldEffect(int block, bool start)
        {
            _holdParticles[block].Stop();
            if (start)
            {
                _holdParticles[block].Play();
            }
        }

        public void JudgeEffect(Judges judge, int block)
        {
            for (int i = 0; i < 6; i++)
            {
                _particles[block][i].Stop();
                _textParticles[block][i].Stop();
                _textParticles[block][i].transform.localScale = Vector3.zero;
            }

            switch (judge)
            {
                case Judges.PurePerfect:
                    _textParticles[block][0].Play();
                    _particles[block][0].Play();
                    break;
                case Judges.Perfect:
                    _textParticles[block][1].Play();
                    _particles[block][1].Play();
                    break;
                case Judges.Great:
                    _textParticles[block][2].Play();
                    _particles[block][2].Play();
                    break;
                case Judges.Good:
                    _textParticles[block][3].Play();
                    _particles[block][3].Play();
                    break;
                case Judges.Bad:
                    _textParticles[block][4].Play();
                    _particles[block][4].Play();
                    break;
                case Judges.Miss:
                    _textParticles[block][5].transform.position = new Vector3(block - 1.5f, 0.55f, 0);
                    _textParticles[block][5].Play();
                    _particles[block][5].Play();
                    break;
                case Judges.Auto:
                case Judges.None:
                default:
                    break;
            }
        }
    }
}