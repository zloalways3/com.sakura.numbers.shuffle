using GameSakuraMainScripts;
using VContainer;
using VContainer.Unity;

namespace GameBootstrapInitializerForSakura
{
    public class GamePresenter : IStartable, ITickable
    {
        [Inject] private readonly GameStateChecker _gameStateChecker;
        [Inject] private readonly GameTimer _gameTimer;
        
        
        public void Start()
        {
            _gameStateChecker.LoadLevelsInfo();
        }

        public void Tick()
        {
            _gameTimer.Tick();
        }
    }
}