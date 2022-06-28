using UnityEngine;

namespace BeeRun
{
    public class GameScreen : BaseScreen
    {
        public void OnBack()
        {
            LevelManager.Instance.Load("home");
        }
    }
}
