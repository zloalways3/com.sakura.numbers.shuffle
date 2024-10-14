using GameSakuraMainScripts;
using UISakuraGameVoews;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameBootstrapInitializerForSakura
{
    public class GameInitializer : LifetimeScope
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private GameSound _gameSound;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameStateChecker>(Lifetime.Singleton);
            builder.Register<UIManagement>(Lifetime.Singleton);
            builder.Register<GameTimer>(Lifetime.Singleton);
        
            builder.RegisterInstance(_gameSettings);
            builder.RegisterInstance(_gameSound);

            builder.RegisterEntryPoint<GamePresenter>();
        }
    }
}
