using UnityEngine;
using VContainer;

namespace GameSakuraMainScripts
{
    public class SoundClipHolder : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        
        private GameSound _gameSound;

        [Inject]
        private void Construct(GameSound gameSound)
        {
            _gameSound = gameSound;
        }

        public void Initialize(GameSound gameSound)
        {
            _gameSound = gameSound;
        }

        public void PlayAudioClip()
        {
            _gameSound.Play(_clip);
        }
    }
}