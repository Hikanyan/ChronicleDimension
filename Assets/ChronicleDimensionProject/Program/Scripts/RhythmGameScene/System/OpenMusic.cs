using System;
using ChronicleDimensionProject.Player;
using UnityEngine;

public class OpenMusic : MonoBehaviour
{
    [SerializeField] private string sceneName = "RhythmGameScene";
    public RhythmGameData musicData;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SelectMusic(RhythmGameData musicdata)
    {
        musicData = musicdata;
        //GameManager.Instance.LoadSceneAsync(sceneName);
    }
}