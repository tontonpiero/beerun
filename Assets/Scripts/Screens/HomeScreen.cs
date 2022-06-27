using UnityEngine;

namespace HomaTest
{
    public class HomeScreen : BaseScreen
    {
        public void OnClickPlay()
        {
            LevelManager.Instance.Load("game");
        }
    }
}
