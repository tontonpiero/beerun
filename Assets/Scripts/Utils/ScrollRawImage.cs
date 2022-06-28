using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeeRun
{
    public class ScrollRawImage : MonoBehaviour
    {
        public Vector2 Speed;

        private RawImage rawImage;

        private void Awake()
        {
            rawImage = GetComponent<RawImage>();
        }

        void Update()
        {
            if (rawImage != null)
            { 
                Rect rect = rawImage.uvRect;
                rect.x += Speed.x * Time.deltaTime;
                rect.y += Speed.y * Time.deltaTime;
                rawImage.uvRect = rect;
            }
        }
    }
}
