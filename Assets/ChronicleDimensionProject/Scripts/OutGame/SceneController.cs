using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController
{
    private Scene _lastScene;
    private readonly Scene _neverUnloadScene;

    //このコンストラクタは、SceneControllerオブジェクトを作成する際に呼び出される特殊なメソッドです。
    public SceneController(Scene neverUnloadScene)
    {
        //Debug.Log(neverUnloadScene.name);
        _neverUnloadScene = neverUnloadScene;
        _lastScene = _neverUnloadScene;
    }

    // 指定したシーンを非同期でロードします。
    public async UniTask LoadScene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
            throw new ArgumentException($"{nameof(scene)} は無効です!");

        await UnloadLastScene();

        await LoadSceneAdditive(scene);
    }

    // 新しいシーンを非同期でロードします。
    public async UniTask LoadNewScene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
            throw new ArgumentException($"{nameof(scene)} は無効です!");

        await UnloadLastScene();

        LoadNewSceneAdditive(scene);
    }

    // シーンを非同期でアンロードします。
    private async UniTask UnloadScene(Scene scene)
    {
        if (!_lastScene.IsValid())
            return;

        var asyncUnload = SceneManager.UnloadSceneAsync(scene);
        await asyncUnload;

        await UniTask.Yield();
    }

    // シーンを非同期で追加ロードします。
    private async UniTask LoadSceneAdditive(string scene)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        await asyncLoad;

        _lastScene = SceneManager.GetSceneByName(scene);
        SceneManager.SetActiveScene(_lastScene);

        // シーンが完全に読み込まれるまで待機する
        while (!_lastScene.isLoaded)
        {
            await UniTask.Yield();
        }
    }

    // 新しいシーンを追加ロードします。
    private void LoadNewSceneAdditive(string sceneName)
    {
        var scene = SceneManager.CreateScene(sceneName);
        SceneManager.SetActiveScene(scene);
        _lastScene = scene;
    }

    // 最後にロードされたシーンを非同期でアンロードします。
    public async UniTask UnloadLastScene()
    {
        if (_lastScene != _neverUnloadScene)
            await UnloadScene(_lastScene);
    }
}