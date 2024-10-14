using GameSakuraMainScripts;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UISakuraGameVoews
{
    public class NextLevelButton : MonoBehaviour
    {
        private GameStateChecker _gameStateChecker;
        private Button _button;
        
        [Inject]
        private void Construct(GameStateChecker gameStateChecker)
        {
            _button = GetComponent<Button>();
            _gameStateChecker = gameStateChecker;
            _button.onClick.AddListener(() => _gameStateChecker.MoveToNextLevel());
            
        }
    }
}