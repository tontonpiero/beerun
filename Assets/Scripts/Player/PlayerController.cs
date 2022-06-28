using System;
using System.Collections;
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

        public event Action<ObstacleBehaviour> OnDeath;
        public event Action<ObstacleBehaviour> OnHit;

        public float ConstantSpeed => constantSpeed;
        public float CurrentSpeed { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsProtected { get; private set; }

        private Vector3 targetPosition;
        private Vector3 velocity;

        private Vector3 turnStartPos;
        private Vector3 turnEndPos;
        private Quaternion turnStartRot;
        private Quaternion turnEndRot;
        private float turnTimeLeft = 0f;
        private const float turnDuration = 1f;

        private void Start()
        {
            targetPosition = body.localPosition;
        }

        public void StartRun()
        {
            CurrentSpeed = constantSpeed;
        }

        private void Update()
        {
            if (!IsDead)
            {
                if (turnTimeLeft > 0f)
                {
                    turnTimeLeft -= Time.deltaTime;
                    float progress = 1f - (turnTimeLeft / turnDuration);

                    Vector3 position = Vector3.Lerp(turnStartPos, turnEndPos, progress);
                    Quaternion direction = Quaternion.Lerp(turnStartRot, turnEndRot, progress);

                    transform.SetPositionAndRotation(position, direction);
                }
                else
                {
                    // Move forward
                    Vector3 position = transform.position;
                    position += CurrentSpeed * Time.deltaTime * transform.forward;
                    transform.position = position;
                }

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

        public void Turn(Vector3 position, Quaternion rotation)
        {
            turnTimeLeft = turnDuration;
            turnStartPos = transform.position;
            turnStartRot = transform.rotation;
            turnEndPos = position;
            turnEndRot = rotation;
        }

        public void HitObstacle(ObstacleBehaviour behaviour)
        {
            if (IsProtected) return;

            switch (behaviour)
            {
                case ObstacleBehaviour.Damage:
                    OnHit?.Invoke(behaviour);
                    StartCoroutine(HitRoutine());
                    break;
                case ObstacleBehaviour.Kill:
                    IsDead = true;
                    targetPosition.y = 1.05f;
                    OnDeath?.Invoke(behaviour);
                    break;
                case ObstacleBehaviour.Web:
                    IsDead = true;
                    targetPosition = body.localPosition;
                    OnDeath?.Invoke(behaviour);
                    break;
            }
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

        private void OnDestroy()
        {
            OnHit = null;
            OnDeath = null;
        }
    }
}
