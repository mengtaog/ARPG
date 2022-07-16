using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 5;

        private void OnTriggerEnter(Collider other)
        {
            var playerStatus = other.GetComponent<PlayerStatus>();

            if (playerStatus != null)
            {
                playerStatus.TakeDamage(damage);
            }
        }
    }
}