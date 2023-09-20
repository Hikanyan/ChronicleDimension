using UnityEngine;

public class OpenMusic : MonoBehaviour
{
    [SerializeField] private string sceneName = "RhythmGameScene";
    public MusicData musicData;

    public void SelectMusic(MusicData musicdata)
    {
        musicData = musicdata;
        GameManager.Instance.LoadSceneAsync(sceneName);
    }
}