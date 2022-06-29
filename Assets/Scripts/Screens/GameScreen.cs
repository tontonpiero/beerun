using UnityEngine;

namespace BeeRun
{
    public class GameScreen : BaseScreen
    {
        public async void OnBack()
        {
            await LevelManager.Instance.LoadAsync("home");
        }
    }
}
