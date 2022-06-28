using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeeRun
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;

        private void OnEnable()
        {
            sfxSlider.value = AudioManager.Instance.SFXGlobalVolume;
            musicSlider.value = AudioManager.Instance.MusicGlobalVolume;
        }

        public void OnSFXVolumeChanged()
        {
            AudioManager.Instance.SFXGlobalVolume = sfxSlider.value;
        }

        public void OnMusicVolumeChanged()
        {
            AudioManager.Instance.MusicGlobalVolume = musicSlider.value;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
