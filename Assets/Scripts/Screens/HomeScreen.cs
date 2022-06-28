using UnityEngine;

namespace BeeRun
{
    public class HomeScreen : BaseScreen
    {
        private void Start()
        {
            AudioManager.Instance.MusicGlobalVolume = 0.5f;
            AudioManager.Instance.PlayMusic("music_home");
        }

        public void OnBack()
        {
            Application.Quit();
        }

        public void OnClickPlay()
        {
            LevelManager.Instance.Load("game");
        }
    }
}
