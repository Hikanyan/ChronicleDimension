﻿using UnityEngine;
using UnityEngine.AddressableAssets;

public class MusicData : MonoBehaviour
{
    [SerializeField] public CriAudioPlayer musicNumber = default;
    [SerializeField] public AssetReferenceT<TextAsset> musicJsonReference;
    [SerializeField] public float delayTime = 0.0f;
}