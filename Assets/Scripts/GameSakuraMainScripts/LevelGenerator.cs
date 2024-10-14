using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace GameSakuraMainScripts
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Item _itemPrefab;

        private GameSettings _settings;
        private GameStateChecker _gameStateChecker;
        private GameSound _gameSound;
        public GameSettings Settings => _settings;

        private List<Item> _levelItems = new List<Item>();
        private Vector2Int _levelSize;
        public Vector2Int LevelSize => _levelSize;
        private bool _isShuffled = false;
        private int _generateNumber = 1000;

        public Action OnLevelPassed;

        [Inject]
        private void Construct(GameStateChecker gameStateChecker, GameSettings settings, GameSound gameSound)
        {
            _settings = settings;
            _gameStateChecker = gameStateChecker;
            _gameSound = gameSound;
            _gameStateChecker.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    NewLevelGenerate();
                    break;
                case GameState.Menu:
                    DeleteOldLevel();
                    break;
            }
        }

        public void NewLevelGenerate()
        {
            DeleteOldLevel();
            _levelSize = _settings.Size;
            List<int> randomValues = new List<int>();
            for (int i = 0; i < _levelSize.x*_levelSize.y-1; i++)
            {
                randomValues.Add(i);
            }
            //randomValues = AdditionalFunc.ShuffleListWithOrderBy(randomValues);
            
            for (int i = 0; i < _levelSize.x; i++)
            {
                for (int j = 0; j < _levelSize.y; j++)
                {
                    if (i != _levelSize.x-1 || j != _levelSize.y-1)
                    {
                        var valueId = 0;
                        Debug.Log(valueId);
                        Item newItem = Instantiate(_itemPrefab, transform);
                        newItem.GetComponent<SoundClipHolder>().Initialize(_gameSound);
                        newItem.Initialize(randomValues[valueId], new Vector2Int(j, i), this);
                        randomValues.RemoveAt(valueId);
                        _levelItems.Add(newItem);
                    }
                }
            }

            StartCoroutine(ShuffleLevelItem());
        }

        private IEnumerator ShuffleLevelItem()
        {
            _isShuffled = true;
            int iterator = 0;
            while (iterator < _generateNumber)
            {
                _levelItems[Random.Range(0, _levelItems.Count)].Move(new Vector2Int(Random.Range(-5, 5), Random.Range(-5, 5)));
                iterator++;
            }
            yield return null;
            _isShuffled = false;
        }

        public void DeleteOldLevel()
        {
            if (_levelItems.Count > 0)
            {
                foreach (var levelItem in _levelItems)
                {
                    Destroy(levelItem.gameObject);
                }
                _levelItems.Clear();
            }
        }
        
        public void CheckWinGame()
        {
            if (_isShuffled)
            {
                return;
            }
            foreach (var levelItem in _levelItems)
            {
                if (!levelItem.IsSuccess)
                {
                    return;
                }
            }

            _gameStateChecker.WinLevel();
        }
        
        public bool CheckFreeCell(Vector2Int position)
        {
            foreach (var levelItem in _levelItems)
            {
                if (levelItem.CurPosition == position || position.x > _levelSize.x-1 || position.y > _levelSize.y-1 || position.x < 0 || position.y < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}