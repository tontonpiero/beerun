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

        // Position variables
        private bool isReachingPosition = false;
        private Action onReachedCallback;
        private Vector3 targetPosition;
        private Vector3 velocity;
        private Quaternion startRotation;
        private Quaternion targetRotation;
        private const float rotationDuration = 0.5f;
        private float rotationTimeleft;

        // Body variables
        private Vector3 targetBodyPosition;
        private Vector3 bodyVelocity;

        // Turn variables
        private Vector3 turnStartPos;
        private Vector3 turnEndPos;
        private Quaternion turnStartRot;
        private Quaternion turnEndRot;
        private const float turnDuration = 1f;
        private float turnTimeLeft = 0f;

        private void Start()
        {
            targetBodyPosition = body.localPosition;
        }

        public void StartRun()
        {
            CurrentSpeed = constantSpeed;
        }

        public void StopRun()
        {
            CurrentSpeed = 0f;
        }

        private void Update()
        {
            UpdateReachPosition();
            UpdateRun();
            UpdateBodyPosition();
        }

        private void UpdateReachPosition()
        {
            if (!isReachingPosition) return;

            // Update rotation
            if (rotationTimeleft > 0f)
            {
                rotationTimeleft -= Time.deltaTime;
                if (rotationTimeleft <= rotationDuration)
                {
                    float progress = 1f - (rotationTimeleft / rotationDuration);
                    transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);
                }
            }

            // Update position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.2f, ConstantSpeed);

            // Check distance to target
            if ((targetPosition - transform.position).magnitude < 0.2f)
            {
                isReachingPosition = false;
                onReachedCallback?.Invoke();
            }
        }

        private void UpdateRun()
        {
            if (IsDead) return;
            if (CurrentSpeed <= 0f) return;
            if (isReachingPosition) return;

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

        private void UpdateBodyPosition()
        {
            Vector3 position = Vector3.SmoothDamp(body.localPosition, targetBodyPosition, ref bodyVelocity, 0.5f);
            position.z = 0f;
            body.localPosition = position;
        }

        public void SetVertical(float value)
        {
            value = Mathf.Clamp(value, -1f, 1f);
            targetBodyPosition.y = MathUtils.Map(value, -1f, 1f, minVertical, maxVertical);
        }

        public void SetHorizontal(float value)
        {
            value = Mathf.Clamp(value, -1f, 1f);
            targetBodyPosition.x = MathUtils.Map(value, -1f, 1f, minHorizontal, maxHorizontal);
        }

        public void Reach(Vector3 position, Quaternion rotation, Action onReached)
        {
            isReachingPosition = true;
            targetPosition = position;
            onReachedCallback = onReached;
            startRotation = transform.rotation;
            targetRotation = rotation;
            rotationTimeleft = rotationDuration + 1.5f;
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
                    targetBodyPosition.y = 1.05f;
                    OnDeath?.Invoke(behaviour);
                    break;
                case ObstacleBehaviour.Web:
                    IsDead = true;
                    targetBodyPosition = body.localPosition;
                    OnDeath?.Invoke(behaviour);
                    break;
            }

            AudioManager.Instance.PlaySound("bee");
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
            onReachedCallback = null;
        }
    }
}
