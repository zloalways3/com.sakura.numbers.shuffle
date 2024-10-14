using GameSakuraMainScripts;
using TMPro;
using UnityEngine;
using VContainer;

namespace UISakuraGameVoews
{
    public class LevelNameView : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [SerializeField] private TMP_Text _levelText;
        
        private GameStateChecker _gameStateChecker;

        [Inject]
        private void Construct(GameStateChecker gameStateChecker)
        {
            _gameStateChecker = gameStateChecker;
            _gameStateChecker.OnStartLevel += OnStartLevel;
        }

        private void OnStartLevel(int index)
        {
            _levelText.text = $"{_levelName} {index+1}";
        }
    }
}