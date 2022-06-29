using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    [CreateAssetMenu(fileName = "SkinData")]
    public class SkinData : ScriptableObject
    {
        [SerializeField] protected List<Material> materials = new List<Material>();

        public Material GetMaterial(int skinIndex)
        {
            if (skinIndex < 0 || skinIndex >= materials.Count) return materials[0];
            return materials[skinIndex];
        }
    }
}
