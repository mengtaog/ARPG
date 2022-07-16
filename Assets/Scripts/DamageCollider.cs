using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class DamageCollider : MonoBehaviour
    {
        public int weaponDamage;
        private Collider collider;


        public void EnableDamageCollider()
        {
            if (collider != null)
            {
                collider.enabled = true;
            }
        }

        public void DisableDamageCollider()
        {
            if (collider != null)
            {
                collider.enabled = false;
            }
        }

        private void Awake()
        {
            collider = GetComponent<Collider>();
            collider.gameObject.SetActive(true);
            collider.isTrigger = true;
            collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                var playerStatus = other.GetComponent<PlayerStatus>();
                if (playerStatus != null)
                {
                    playerStatus.TakeDamage(weaponDamage);
                }
            }
            else if (other.tag == "Enemy")
            {
                var enemyStatus = other.GetComponent<EnemyStatus>();
                if (enemyStatus != null)
                {
                    enemyStatus.TakeDamage(weaponDamage);
                }
            }
        }


    }
}