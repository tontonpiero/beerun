using UnityEngine;

namespace BeeRun
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        static private AudioManager instance;

        [SerializeField] private AudioLibrary library;

        private float _MusicGlobalVolume = 1f;

        private AudioSource musicSource;

        public float SFXGlobalVolume { get; set; } = 1f;
        public float MusicGlobalVolume
        {
            get => _MusicGlobalVolume;
            set
            {
                _MusicGlobalVolume = value;
                musicSource.volume = _MusicGlobalVolume;
            }
        }

        static public AudioManager Instance => instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource = GetComponent<AudioSource>();
        }

        

        public void PlaySound(string name, Vector3 position, float volume = 1f)
        {
            AudioClip clip = library.GetSound(name);
            if (clip != null)
                AudioSource.PlayClipAtPoint(clip, position, volume * SFXGlobalVolume);
        }

        public void PlaySound(string name, float volume = 1f)
        {
            PlaySound(name, Camera.main.transform.position, volume);
        }

        public void PlayMusic(string name)
        {
            AudioClip clip = library.GetSound(name);
            if (clip != null)
            {
                musicSource.volume = _MusicGlobalVolume;
                if (clip != musicSource.clip)
                {
                    musicSource.clip = library.GetSound(name);
                    musicSource.Play();
                }
            }
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }
    }

}