using GameSakuraMainScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISakuraGameVoews
{
    [RequireComponent(typeof(Button))]
    public class LevelSelectButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        private LevelModel _mLevelModel;

        public void Initialize(LevelsInfo levelsInfo, int index)
        {
            _mLevelModel = levelsInfo.LevelCompleted[index];
            _mLevelModel.OnWinLevel += CheckOpenLevel;
            _text.text = (index+1).ToString();
            _button.onClick.AddListener(_mLevelModel.StartLevel);
            CheckOpenLevel(_mLevelModel);
        }

        private void CheckOpenLevel(LevelModel levelModel)
        {
            _button.interactable = _mLevelModel.IsOpened;
        }

        private void OnEnable()
        {
            if(_mLevelModel != null) CheckOpenLevel(_mLevelModel);
        }
    }
}