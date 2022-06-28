using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private PlayerController controller;

        private int SpeedParam = Animator.StringToHash("Speed");
        private int DeathTrigger = Animator.StringToHash("Death");
        private int HitTrigger = Animator.StringToHash("Hit");
        private int ObstacleParam = Animator.StringToHash("Obstacle");

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            controller.OnHit += PlayHitAnimation;
            controller.OnDeath += PlayDeathAnimation;
        }

        public void PlayHitAnimation(ObstacleBehaviour behaviour)
        {
            animator.SetTrigger(HitTrigger);
        }

        public void PlayDeathAnimation(ObstacleBehaviour behaviour)
        {
            animator.SetInteger(ObstacleParam, (int)behaviour);
            animator.SetTrigger(DeathTrigger);
        }

        private void Update()
        {
            animator.SetFloat(SpeedParam, controller.CurrentSpeed / controller.ConstantSpeed);
        }
    }
}
