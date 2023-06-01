using Hikanyan.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan.Gameplay
{
    /// <summary>
    /// ���̃V���O���g���́A�ώ@���ꂽ�Q�[����C�x���g�Ɋ�Â��ăQ�[���̏�Ԃ����肵�܂��B
    /// </summary>
    public class SequenceManager : AbstractSingleton<SequenceManager>
    {
        [SerializeField]
        GameObject[] _preloadedAssets;
        public void Initialize()
        {
            InstantiatePreloadedAssets();
        }
        void InstantiatePreloadedAssets()
        {
            foreach (var asset in _preloadedAssets)
            {
                Instantiate(asset);
            }
        }
    }
}
