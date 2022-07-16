using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class EnemyAnimatorHandler : AnimatorHandler
    {
        public EnemyManager enemyManager;

        private void Awake()
        {
            enemyManager = GetComponentInParent<EnemyManager>();
            animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemeyRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemeyRigidbody.velocity = velocity;
        }
    }
}
