using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSakuraMainScripts
{
    public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _highlightColor;

        private Transform _currTransform;
        private LevelGenerator _levelGenerator;
        private SoundClipHolder _soundClipHolder;
        public SoundClipHolder SoundClipHolder => _soundClipHolder;

        private Vector2Int _curPosition;
        public Vector2Int CurPosition => _curPosition;
        private int _value = 0;

        private Vector2Int _deltaDir;
        private Vector2 _mousePosition;

        private bool _isSuccess;
        public bool IsSuccess => _isSuccess;

        private void Awake()
        {
            _currTransform = transform;
            _soundClipHolder = GetComponent<SoundClipHolder>();
        }

        public void Initialize(int value, Vector2Int position, LevelGenerator levelGenerator)
        {
            _value = value;
            _levelGenerator = levelGenerator;
            _text.text = (_value+1).ToString();
            SetNewPosition(position);
        }

        private void SetNewPosition(Vector2Int position)
        {
            _curPosition = position;
            Vector3 newPosition = 
                new Vector3(_curPosition.x - _levelGenerator.LevelSize.x/2f + _levelGenerator.Settings.Offset.x, 
                    _levelGenerator.LevelSize.y/2f + _levelGenerator.Settings.Offset.y - _curPosition.y);
            _currTransform.DOMove(newPosition, 0.25f, false).SetEase(Ease.Linear);
            if (_levelGenerator.LevelSize.x * _curPosition.y + _curPosition.x == _value)
            {
                _spriteRenderer.color = _highlightColor;
                _isSuccess = true;
            }
            else
            {
                _spriteRenderer.color = _baseColor;
                _isSuccess = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _mousePosition = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            int distanceX = (int)(eventData.position.x - _mousePosition.x);
            int distanceY = (int)(eventData.position.y - _mousePosition.y);
            Move(new Vector2Int(distanceX, distanceY));
            _soundClipHolder.PlayAudioClip();
        }

        public void Move(Vector2Int direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                _deltaDir = 
                    new Vector2Int(Mathf.Clamp(direction.x, -1, 1), 0);
            }
            else
            {
                _deltaDir = 
                    new Vector2Int(0, -Mathf.Clamp(direction.y, -1, 1));
            }
            
            Debug.Log(_deltaDir);
            if (_levelGenerator.CheckFreeCell(_curPosition + _deltaDir))
            {
                SetNewPosition(_curPosition + _deltaDir);
            }
            _levelGenerator.CheckWinGame();
        }
    }
}