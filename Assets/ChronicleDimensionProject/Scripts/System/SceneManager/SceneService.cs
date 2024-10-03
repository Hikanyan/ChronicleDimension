using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.Common
{
    public class SceneService
    {
        private readonly SceneLoader _sceneLoader;

        public SceneService(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask LoadScene(string sceneName)
        {
            await _sceneLoader.LoadSceneAsync(sceneName);
        }

        public async UniTask UnloadScene(string sceneName)
        {
            await _sceneLoader.UnloadSceneAsync(sceneName);
        }

        public bool IsSceneLoaded(string sceneName)
        {
            return _sceneLoader.IsSceneLoaded(sceneName);
        }
    }
}