using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class SkinEditorItem : MonoBehaviour
    {
        [SerializeField] private GameObject selectedObject;

        public void SetSelected(bool selected)
        {
            selectedObject.SetActive(selected);
        }
    }
}
