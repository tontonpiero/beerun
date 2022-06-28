using UnityEngine;

namespace BeeRun
{
    public class GameScreen : BaseScreen
    {
        private void Start()
        {
            LevelManager.Instance.Load("level_01", true);
        }

        public void OnBack()
        {
            LevelManager.Instance.Load("home");
        }
    }
}
