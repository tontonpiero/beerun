using UnityEngine;

namespace BeeRun
{
    public class HomeScreen : BaseScreen
    {
        public void OnClickPlay()
        {
            LevelManager.Instance.Load("game");
        }
    }
}
