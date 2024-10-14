using GameSakuraMainScripts;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UISakuraGameVoews
{
    [RequireComponent(typeof(Button))]
    public class ButtonSwapMenu : MonoBehaviour
    {
        [SerializeField] private Button m_button;
        [SerializeField] private GameState _toMenuState;
        [SerializeField] private bool backButton = false;

        [Inject] private readonly GameStateChecker _gameStateChecker;

        private void Awake()
        {
            m_button.onClick.AddListener(SwapMenu);
        }

        private void SwapMenu()
        {
            if (backButton)
            {
                _gameStateChecker.BackToPreviousState();
            }
            else
            {
                _gameStateChecker.GameStateChange(_toMenuState);
            }
            
        }
    }
}