using UnityEngine;

namespace HomaTest
{
    public interface IAudioManager
    {
        float MusicGlobalVolume { get; set; }
        float SFXGlobalVolume { get; set; }

        void PlaySound(string name, float volume = 1);
        void PlaySound(string name, Vector3 position, float volume = 1);
        void PlayMusic(string name);
        void StopMusic();
    }
}