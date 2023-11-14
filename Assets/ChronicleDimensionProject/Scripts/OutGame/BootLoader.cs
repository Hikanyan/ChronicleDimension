using System.Collections;
using System.Collections.Generic;
using ChronicleDimensionProject.Scripts.OutGame;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 開始時に SequenceManager をインスタンス化して初期化します。
/// </summary>
public class BootLoader : MonoBehaviour
{
    [SerializeField] SequenceManager sequenceManagerPrefab;

    void Awake()
    {
        Instantiate(sequenceManagerPrefab);
        SequenceManager.Instance.Initialize();
    }
}