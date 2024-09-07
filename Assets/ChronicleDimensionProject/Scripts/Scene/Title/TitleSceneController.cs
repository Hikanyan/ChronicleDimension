using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using SceneManager = ChronicleDimensionProject.System.SceneManager;

namespace ChronicleDimensionProject.Title
{
    public class TitleSceneController : IStartable
    {
        private readonly SceneManager _sceneManager;

        [Inject]
        public TitleSceneController(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public void Start()
        {
            // TitleSceneの起動時にManagerSceneをロードする
            StartGameAsync().Forget();
        }

        private async UniTaskVoid StartGameAsync()
        {
            await _sceneManager.LoadSceneAsync("TitleScene", LoadSceneMode.Additive);
        }
    }

    public class GameSceneController : IStartable
    {
        private readonly SceneManager _sceneManager;

        [Inject]
        public GameSceneController(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public void Start()
        {
            // GameSceneの起動時にManagerSceneをロードする
            StartGameAsync().Forget();
        }

        private async UniTaskVoid StartGameAsync()
        {
            await _sceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        }
    }
}