using UnityEngine;

namespace BeeRun
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private string soundName;
        [SerializeField] private float volume = 1f;
        [SerializeField] private bool playAtPosition = false;
        [SerializeField] private bool playOnStart = false;

        private void Start()
        {
            if (playOnStart) Play();
        }

        public void Play()
        {
            if (string.IsNullOrEmpty(soundName)) return;
            if (playAtPosition)
            {
                AudioManager.Instance.PlaySound(soundName, transform.position, volume);
            }
            else
            {
                AudioManager.Instance.PlaySound(soundName, volume);
            }
        }
    }
}
