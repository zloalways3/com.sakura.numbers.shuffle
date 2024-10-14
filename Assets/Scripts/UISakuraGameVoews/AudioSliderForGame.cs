using UnityEngine;
using UnityEngine.UI;

namespace UISakuraGameVoews
{
    public class AudioSliderForGame : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private string _key;
        [SerializeField] private string _muteKey;

        private static int _volume;
        private static float _loudness;

        private void Awake()
        {
            _musicSlider.onValueChanged.AddListener(SlideAudioSourceMusic);
            SlideAudioSourceMusic(PlayerPrefs.GetFloat(_key, 1));
            _musicSlider.value = PlayerPrefs.GetFloat(_key, 1);
        }

        public void SlideAudioSourceMusic(float val)
        {
            _loudness = val;
            PlayerPrefs.SetFloat(_key, _loudness);
            _musicAudio.volume = _loudness;
        }
    }
}