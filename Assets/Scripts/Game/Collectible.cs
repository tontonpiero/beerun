using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    public class Collectible : MonoBehaviour
    {
        public const string PlayerTag = "Player";

        [SerializeField] private CollectibleType type;
        [SerializeField] private GameObject fxPrefab;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PlayerTag))
            {
                GameManager.Instance.Collect(type);
                PlayFX();
                Destroy(gameObject);
            }
        }

        private void PlayFX()
        {
            if (fxPrefab != null)
            {
                Instantiate(fxPrefab, transform.position, transform.rotation);
            }
        }
    }

    [Serializable]
    public enum CollectibleType
    {
        Coin,
        Flower
    }
}
