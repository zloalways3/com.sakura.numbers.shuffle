using System;
using UnityEngine;
using VContainer;

namespace GameSakuraMainScripts
{
    public class GameTimer
    {
        private GameStateChecker _gameStateChecker;
        
        private float _currentTime;
        public float CurrentTime => _currentTime;
        private bool _isStarted;
        public bool IsStarted => _isStarted;
        
        public Action<float> OnTimeUpdate;

        [Inject]
        private GameTimer(GameStateChecker gameStateChecker)
        {
            _gameStateChecker = gameStateChecker;
            _gameStateChecker.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    _currentTime = 0;
                    break;
                case GameState.Game:
                    _isStarted = true;
                    break;
                default:
                    _isStarted = false;
                    break;
            }
        }

        public void Tick()
        {
            if (_isStarted)
            {
                _currentTime += Time.deltaTime;
                OnTimeUpdate?.Invoke(_currentTime);
            }
        }
    }
}