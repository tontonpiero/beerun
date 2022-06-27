using UnityEngine;

namespace HomaTest
{
    public class GameScreen : BaseScreen
    {
        public void OnBack()
        {
            LevelManager.Instance.Load("home");
        }
    }
}
