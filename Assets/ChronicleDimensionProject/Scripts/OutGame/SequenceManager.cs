using Hikanyan.Core;
using UnityEngine;

namespace ChronicleDimensionProject.Scripts.OutGame
{
    /// <summary>
    /// ���̃V���O���g���́A�ώ@���ꂽ�Q�[����C�x���g�Ɋ�Â��ăQ�[���̏�Ԃ����肵�܂��B
    /// </summary>
    public class SequenceManager : AbstractSingleton<SequenceManager>
    {
        [SerializeField] GameObject[] _preloadedAssets;
        /// <summary>
        /// ������
        /// </summary>
        public void Initialize()
        {
            InstantiatePreloadedAssets();
        }

        /// <summary>
        /// �o�^���ꂽPrefab��S�ăC���X�^���X��
        /// </summary>
        void InstantiatePreloadedAssets()
        {
            foreach (var asset in _preloadedAssets)
            {
                Instantiate(asset);
            }
        }
    }
}