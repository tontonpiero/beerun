using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeeRun
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private TMP_Text versionText;
        [SerializeField] private GameObject homeUI;

        private void OnEnable()
        {
            sfxSlider.value = AudioManager.Instance.SFXGlobalVolume;
            musicSlider.value = AudioManager.Instance.MusicGlobalVolume;

            versionText.text = "Version " + Application.version;
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
            homeUI.SetActive(false);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            homeUI.SetActive(true);
        }
    }
}
