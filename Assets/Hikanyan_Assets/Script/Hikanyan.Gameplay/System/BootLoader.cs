using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Hikanyan.Gameplay
{
    /// <summary>
    /// 開始時に SequenceManager をインスタンス化して初期化します。
    /// </summary>
    public class BootLoader : MonoBehaviour
    {
        [SerializeField]
        SequenceManager sequenceManagerPrefab;

        void Start()
        {
            Instantiate(sequenceManagerPrefab);
            SequenceManager.Instance.Initialize();
        }
    }
}