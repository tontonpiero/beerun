using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomaTest
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.PlayMusic("music_01");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlaySound("click_01");
            }
            if (Input.GetMouseButtonDown(1))
            {
                LevelManager.Instance.Restart();
            }
        }
    }
}
