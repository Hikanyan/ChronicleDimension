using Hikanyan.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan.Gameplay
{
    /// <summary>
    /// このシングルトンは、観察されたゲームやイベントに基づいてゲームの状態を決定します。
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
