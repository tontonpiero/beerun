using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private SkinData skinData;

        private void Start()
        {
            ApplySkin();
        }

        public void ApplySkin()
        {
            int skinIndex = UserManager.Instance.SkinIndex;
            Material mat = skinData.GetMaterial(skinIndex);

            targetRenderer.material = mat;
        }
    }
}
