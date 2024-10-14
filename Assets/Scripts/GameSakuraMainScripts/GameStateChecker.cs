using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameSakuraMainScripts
{
    public enum GameState
    {
        Menu,
        Options,
        Game,
        Start,
        Levels,
        Win,
        Pause,
        Exit,
    }
    
    [Serializable]
    public class LevelsInfo
    {
        [SerializeField] private List<LevelModel> levelCompleted = new List<LevelModel>();
        public List<LevelModel> LevelCompleted => levelCompleted;

        public LevelsInfo(int levelCount)
        {
            for (int i = 0; i < levelCount; i++)
            {
                levelCompleted.Add(new LevelModel(i==0));
            }
            Debug.Log(levelCompleted.Count);
        }
    }

    [Serializable]
    public class LevelModel
    {
        [SerializeField]  private bool _isCompleted = false;
        public bool IsCompleted => _isCompleted;
        [SerializeField] private bool _isOpened = false;
        public bool IsOpened => _isOpened;

        public Action<LevelModel> OnStartLevel;
        public Action<LevelModel> OnWinLevel;
        
        public LevelModel(bool isOpened)
        {
            _isOpened = isOpened;
        }

        public void StartLevel()
        {
            OnStartLevel?.Invoke(this);
        }

        public void LevelCompleted()
        {
            _isCompleted = true;
            OnWinLevel?.Invoke(this);
        }

        public void OpenLevel()
        {
            _isOpened = true;
        }
    }
    
    public class GameStateChecker
    {
        private GameSettings _settings;
        private LevelsInfo _levelsInfo;
        private LevelModel _currentLevelModel;
        public LevelsInfo LevelsInfo => _levelsInfo;
        private GameState _gameState = GameState.Menu;
        public GameState GameState => _gameState;
        private GameState _lastState = GameState.Menu;

        public Action<GameState> OnGameStateChanged;
        public Action<int> OnStartLevel;
        public Action<LevelsInfo> OnLevelLoaded;
        
        [Inject]
        private GameStateChecker(GameSettings settings)
        {
            _settings = settings;
            OnGameStateChanged += GameStateChanged;
        }

        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    GameStateChange(GameState.Game);
                    break;
            }
        }

        public void SaveLevelsInfo()
        {
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(_levelsInfo));
        }

        public void OpenNextLevel()
        {
            var nextLevel = _levelsInfo.LevelCompleted[Mathf.Clamp(_levelsInfo.LevelCompleted.IndexOf(_currentLevelModel)+1, 0, _levelsInfo.LevelCompleted.Count)];
            nextLevel.OpenLevel();
            SaveLevelsInfo();
        }

        public void LoadLevelsInfo()
        {
            //PlayerPrefs.DeleteAll();
            Debug.Log("Loading levels info...");
            if (PlayerPrefs.HasKey("Save"))
            {
                Debug.Log(PlayerPrefs.GetString("Save"));
                _levelsInfo = JsonUtility.FromJson<LevelsInfo>(PlayerPrefs.GetString("Save"));
            }
            else
            {
                _levelsInfo = new LevelsInfo(_settings.LevelCount);
            }
            foreach (var level in _levelsInfo.LevelCompleted)
            {
                level.OnStartLevel += InitStartGame;
            }
            OnLevelLoaded?.Invoke(_levelsInfo);
        }

        public void WinLevel()
        {
            _currentLevelModel.LevelCompleted();
            OpenNextLevel();
            GameStateChange(GameState.Win);
        }

        public void MoveToNextLevel()
        {
            var nextLevel = _levelsInfo.LevelCompleted[Mathf.Clamp(_levelsInfo.LevelCompleted.IndexOf(_currentLevelModel)+1, 0, _levelsInfo.LevelCompleted.Count)];
            InitStartGame(nextLevel);
        }
        
        public void InitStartGame(LevelModel levelModel)
        {
            _currentLevelModel = levelModel;
            OnStartLevel?.Invoke(_levelsInfo.LevelCompleted.IndexOf(_currentLevelModel));
            GameStateChange(GameState.Start);
        }

        public void GameStateChange(GameState gameState)
        {
            _lastState = _gameState;
            _gameState = gameState;
            Debug.Log(_lastState + "----" + _gameState);
            OnGameStateChanged?.Invoke(_gameState);
        }
        
        public void BackToPreviousState()
        {
            GameStateChange(_lastState);
        }
        
    }
}