using System.Collections.Generic;
using GameSakuraMainScripts;
using VContainer;

namespace UISakuraGameVoews
{
    public class UIManagement
    {
        private GameStateChecker _gameStateChecker;
        
        private List<MainPanel> m_panels = new List<MainPanel>();
        
        [Inject]
        private UIManagement(GameStateChecker gameStateChecker)
        {
            _gameStateChecker = gameStateChecker;
            _gameStateChecker.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            foreach (var varPanel in m_panels)
            {
                if (varPanel.GameState.Contains(gameState))
                {
                    varPanel.Show();
                    
                }
                else
                {
                    varPanel.Hide();
                }
            }
        }

        public void AddMenuToPool(MainPanel mainPanel)
        {
            m_panels.Add(mainPanel);
            OnGameStateChanged(_gameStateChecker.GameState);
        }
    }
}