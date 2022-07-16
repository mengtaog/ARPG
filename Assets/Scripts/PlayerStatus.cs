using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class PlayerStatus : CharacterStatus
    {

        public HealthBar healthBar;
        private AnimationHandler animationHandler;
        

        public void TakeDamage(int damage)
        {
            if (isAlive == false) return;

            health -= damage;
            if (health <= 0)
            {
                health = 0;
                animationHandler.SetTrigger("Die");
                isAlive = false;
            }
            animationHandler.SetTrigger("GetHit");
            healthBar.SetHealth(health);
            
        }
        // Start is called before the first frame update
        private void Start()
        {
            animationHandler = GetComponentInChildren<AnimationHandler>();
            health = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(health);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
