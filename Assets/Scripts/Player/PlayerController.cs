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
        [SerializeField] private SkinnedMeshRenderer meshRenderer;

        public event Action OnDeath;
        public event Action OnHit;

        public float CurrentSpeed { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsProtected { get; private set; }

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
            if (!IsDead)
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
            }

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

        public void TakeDamage()
        {
            if (IsProtected) return;
            OnHit?.Invoke();

            StartCoroutine(HitRoutine());
        }

        private IEnumerator HitRoutine()
        {
            IsProtected = true;
            float timeLeft = 2f;
            while (timeLeft > 0f)
            {
                meshRenderer.enabled = ((int)(timeLeft * 10)) % 2 == 0;
                timeLeft -= Time.deltaTime;
                yield return null;
            }
            IsProtected = false;
            meshRenderer.enabled = true;
        }

        public void Kill()
        {
            if (IsProtected) return;
            IsDead = true;
            targetPosition.y = 1.05f;
            OnDeath?.Invoke();
        }

        private void OnDestroy()
        {
            OnHit = null;
            OnDeath = null;
        }
    }
}
