using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class SkinEditorUI : MonoBehaviour
    {
        [SerializeField] private SkinEditorItem[] items;
        [SerializeField] private PlayerSkin playerSkin;
        [SerializeField] private GameObject homeUI;

        public void Show()
        {
            gameObject.SetActive(true);
            homeUI.SetActive(false);

            SetSelectedIndex(UserManager.Instance.SkinIndex);
        }

        public void OnItemSelected(int skinIndex)
        {
            UserManager.Instance.SetSkin(skinIndex);
            SetSelectedIndex(skinIndex);
            playerSkin.ApplySkin();
        }

        private void SetSelectedIndex(int skinIndex)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].SetSelected(i == skinIndex);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            homeUI.SetActive(true);
        }
    }
}
