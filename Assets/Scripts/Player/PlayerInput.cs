using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private float horizontalFactor = 8f;
        [SerializeField] private float verticalFactor = 14f;

        private PlayerController controller;
        private Vector3 origin;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (controller.IsDead) return;
            if (GameManager.Instance.State != GameState.Started) return;
            if (Input.GetMouseButtonDown(0))
            {
                origin = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 delta = (Input.mousePosition - origin) / Screen.width;
                controller.SetHorizontal(delta.x * horizontalFactor);
                controller.SetVertical(delta.y * verticalFactor);
            }
        }
    }
}
