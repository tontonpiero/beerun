using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class Hive : MonoBehaviour
    {
        [SerializeField] private Transform startTransform;
        [SerializeField] private float stepHeight = 2.5f;
        [SerializeField] private HiveStep[] steps;

        private int flowerCount;
        private PlayerController controller;
        private Action onCompleteCallback;
        private bool isReachingEnd = false;
        private int currentStep = -1;

        public void StartEndAnimation(int flowerCount, PlayerController controller, Action onComplete)
        {
            onCompleteCallback = onComplete;
            this.flowerCount = flowerCount;
            this.controller = controller;

            ReachStartPosition();
        }

        private void ReachStartPosition()
        {
            controller.SetHorizontal(0f);
            controller.SetVertical(-0.8f);
            controller.Reach(startTransform.position, startTransform.rotation, OnStartPositionReached);
        }

        private void OnStartPositionReached()
        {
            controller.StopRun();
            Vector3 targetPosition = startTransform.position + Vector3.up * flowerCount * stepHeight;
            controller.Reach(targetPosition, startTransform.rotation, OnEndPositionReached);
            isReachingEnd = true;
        }

        private void OnEndPositionReached()
        {
            isReachingEnd = false;
            onCompleteCallback?.Invoke();
        }

        private void Update()
        {
            if (isReachingEnd)
            {
                int step = Mathf.FloorToInt((controller.transform.position.y + 1f) / stepHeight);
                if (step > currentStep)
                {
                    currentStep = step;
                    if (currentStep < steps.Length)
                    {
                        steps[currentStep].Highlight(currentStep == flowerCount);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            onCompleteCallback = null;
        }
    }
}
