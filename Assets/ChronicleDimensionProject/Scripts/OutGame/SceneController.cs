using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController
{
    private Scene _lastScene;
    private readonly Scene _neverUnloadScene;
    /// <summary>
    //このコンストラクタは、SceneControllerオブジェクトを作成する際に呼び出される特殊なメソッドです。
    /// </summary>
    /// <param name="neverUnloadScene"></param>
    public SceneController(Scene neverUnloadScene)
    {
        //Debug.Log(neverUnloadScene.name);
        _neverUnloadScene = neverUnloadScene;
        _lastScene = _neverUnloadScene;
    }

    /// <summary>
    /// LoadSceneは、シーンをアンロードせずに指定したシーンを非同期でロードすることです。
    /// </summary>
    /// <param name="scene"></param>
    /// <exception cref="ArgumentException"></exception>
    public async UniTask LoadScene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
            throw new ArgumentException($"{nameof(scene)} は無効です!");

        await UnloadLastScene();

        await LoadSceneAdditive(scene);
    }

    /// <summary>
    /// LoadNewSceneは、シーンをアンロードしてから指定したシーンを非同期でロードすることです。
    /// </summary>
    /// <param name="scene"></param>
    /// <exception cref="ArgumentException"></exception>
    public async UniTask LoadNewScene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
            throw new ArgumentException($"{nameof(scene)} は無効です!");

        await UnloadLastScene();

        LoadNewSceneAdditive(scene);
    }

    /// <summary>
    /// シーンを非同期でアンロードします。
    /// </summary>
    /// <param name="scene"></param>
    private async UniTask UnloadScene(Scene scene)
    {
        if (!_lastScene.IsValid())
            return;

        var asyncUnload = SceneManager.UnloadSceneAsync(scene);
        await asyncUnload;

        await UniTask.Yield();
    }

    /// <summary>
    /// シーンを非同期で追加ロードします。
    /// </summary>
    /// <param name="scene"></param>
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

    /// <summary>
    /// 新しいシーンを追加ロードします。
    /// </summary>
    /// <param name="sceneName"></param>
    private void LoadNewSceneAdditive(string sceneName)
    {
        var scene = SceneManager.CreateScene(sceneName);
        SceneManager.SetActiveScene(scene);
        _lastScene = scene;
    }

    /// <summary>
    /// 最後にロードされたシーンを非同期でアンロードします。
    /// </summary>
    public async UniTask UnloadLastScene()
    {
        if (_lastScene != _neverUnloadScene)
            await UnloadScene(_lastScene);
    }
}