using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float constantSpeed = 5f;
        [SerializeField] private float minVertical = 0.5f;
        [SerializeField] private float maxVertical = 3.5f;
        [SerializeField] private float minHorizontal = -3f;
        [SerializeField] private float maxHorizontal = 3f;
        [SerializeField] private Transform body;

        public event Action OnDeath;
        public event Action OnHit;

        public float CurrentSpeed { get; private set; }
        public bool IsDead { get; private set; }

        private Vector3 targetPosition;
        private Vector3 velocity;

        private void Start()
        {
            targetPosition = body.localPosition;
        }

        public void Move()
        {
            CurrentSpeed = constantSpeed;
        }

        private void Update()
        {
            // temp
            if (CurrentSpeed == 0f && Input.GetKeyDown(KeyCode.Space))
            {
                Move();
            }

            // Move forward
            Vector3 position = transform.position;
            position += CurrentSpeed * Time.deltaTime * transform.forward;
            transform.position = position;

            UpdateBodyPosition();
        }

        private void UpdateBodyPosition()
        {
            Vector3 position = Vector3.SmoothDamp(body.localPosition, targetPosition, ref velocity, 0.5f);
            position.z = 0f;
            body.localPosition = position;
        }

        public void SetVertical(float value)
        {
            value = Mathf.Clamp(value, -1f, 1f);
            targetPosition.y = MathUtils.Map(value, -1f, 1f, minVertical, maxVertical);
        }

        public void SetHorizontal(float value)
        {
            value = Mathf.Clamp(value, -1f, 1f);
            targetPosition.x = MathUtils.Map(value, -1f, 1f, minHorizontal, maxHorizontal);
        }

        public void Turn(float angle)
        {
            transform.Rotate(Vector3.up, angle);
        }

        private void OnDestroy()
        {
            OnHit = null;
            OnDeath = null;
        }
    }
}
