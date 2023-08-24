using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// �J�n���� SequenceManager ���C���X�^���X�����ď��������܂��B
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