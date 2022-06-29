using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class PlayerFX : MonoBehaviour
    {
        [SerializeField] private GameObject webFXPrefab;
        [SerializeField] private GameObject cocoonFXPrefab;
        [SerializeField] private Transform fxParent;

        private PlayerController controller;

        private GameObject cocoon;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            controller.OnHit += PlayHitFX;
            controller.OnDeath += PlayDeathFX;
        }

        private void PlayDeathFX(ObstacleBehaviour behaviour)
        {
            switch (behaviour)
            {
                case ObstacleBehaviour.Damage:
                    break;
                case ObstacleBehaviour.Kill:
                    break;
                case ObstacleBehaviour.Web:
                    Instantiate(webFXPrefab, fxParent, false);
                    cocoon = Instantiate(cocoonFXPrefab, fxParent, false);
                    break;
            }
        }

        private void PlayHitFX(ObstacleBehaviour behaviour)
        {
            CleanUpFX();

            switch (behaviour)
            {
                case ObstacleBehaviour.Damage:
                    break;
                case ObstacleBehaviour.Kill:
                    break;
                case ObstacleBehaviour.Web:
                    break;
            }
        }

        private void CleanUpFX()
        {
            if (cocoon != null) Destroy(cocoon);
            cocoon = null;
        }
    }
}
