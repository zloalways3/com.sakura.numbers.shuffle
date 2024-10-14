using GameSakuraMainScripts;
using UnityEngine;
using VContainer;

namespace UISakuraGameVoews
{
    public class LevelSelectPanel : MonoBehaviour
    {
        [SerializeField] private LevelSelectButton _levelSelectButtonPrefab;

        [Inject]
        private void Container(GameStateChecker gameStateChecker)
        {
            gameStateChecker.OnLevelLoaded += LoadingSuccess;
        }
        
        private void LoadingSuccess(LevelsInfo levelsInfo)
        {
            Debug.Log("Create level: " + levelsInfo.LevelCompleted.Count);
            for (int i = 0; i < levelsInfo.LevelCompleted.Count; i++)
            {
                
                LevelSelectButton newLevelButton = Instantiate(_levelSelectButtonPrefab, transform);
                newLevelButton.Initialize(levelsInfo, i);
            }
        }
    }
}