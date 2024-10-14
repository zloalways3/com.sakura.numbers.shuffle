using UnityEngine;

namespace GameSakuraMainScripts
{
    [RequireComponent(typeof(AudioSource))]
    public class GameSound : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            _audioSource.volume = PlayerPrefs.GetFloat("SoundVolume");
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(clip);
        }
    }
}