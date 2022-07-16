using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MT
{

    public class EnemyStatus : MonoBehaviour
    {
        public int maxHealth = 100;
        public int health;
        
        private Animator animator;
        private bool isAlive = true;

        public void TakeDamage(int damage)
        {
            if (isAlive == false) return;

            health -= damage;
            if (health <= 0)
            {
                health = 0;
                animator.SetTrigger("Die");
                isAlive = false;
            }
            animator.SetTrigger("GetHit");


        }
        // Start is called before the first frame update
        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            health = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

