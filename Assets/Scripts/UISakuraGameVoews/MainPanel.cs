using System.Collections.Generic;
using DG.Tweening;
using GameSakuraMainScripts;
using UnityEngine;
using VContainer;

namespace UISakuraGameVoews
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] protected List<GameState> _gameState;
        public List<GameState> GameState => _gameState;
        protected bool isAnimation = false;
        
        [Inject]private readonly UIManagement _uiManagement;
        

        private void Start()
        {
            _uiManagement.AddMenuToPool(this);
        }

        public void Show()
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
                gameObject.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(OnShowed);
            }
            
        }

        public void OnShowed()
        {
            isAnimation = false;
        }

        public void Hide()
        {
            if (gameObject.activeInHierarchy)
            {
                isAnimation = true;
                gameObject.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Linear).OnComplete(UnActive);
            }
        }

        protected void UnActive()
        {
            isAnimation = false;
            gameObject.SetActive(false);
        }
    }
}