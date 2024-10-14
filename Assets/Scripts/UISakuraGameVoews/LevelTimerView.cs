using GameSakuraMainScripts;
using TMPro;
using UnityEngine;
using VContainer;

namespace UISakuraGameVoews
{
    public class LevelTimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private string _timerName;
        
        private GameTimer _gameTimer;
        
        [Inject]
        private void Construct(GameTimer gameTimer)
        {
            _gameTimer = gameTimer;
            _gameTimer.OnTimeUpdate += OnTimeUpdate;
        }

        private void OnTimeUpdate(float time)
        {
            _text.text = $"{_timerName} {Mathf.RoundToInt(time / 60):00}:{Mathf.RoundToInt(time % 60):00}";
        }

        private void OnEnable()
        {
            OnTimeUpdate(_gameTimer.CurrentTime);
        }
    }
}