using UnityEngine;

namespace BeeRun
{
    public class LoadingScreen : BaseScreen
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LevelManager.Instance.Load("home");
            }
        }
    }
}
