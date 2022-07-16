using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class PursueTargetState : State
    {
        public CombatStanceState combatStanceState;

        public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStatus, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //chase target
            //if within attack range, switch to combat stance state
            if (enemyManager.isPerformingAction)
            {
                return this;
            }


            Vector3 targetDirection = enemyManager.currentTargetStatus.transform.position - transform.position;
            float viewAngle = Vector3.Angle(targetDirection, transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTargetStatus.transform.position, transform.position);
            if (distanceFromTarget > enemyManager.maxAttackRange)
            {
                enemyAnimatorHandler.SetFloat("MovementSpeed", 2, 0.1f, Time.deltaTime);
            }

            HandleRotateToTarget(enemyManager);

            enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
            enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;

            if (enemyManager.distanceFromTarget < enemyManager.maxAttackRange)
            {
                return combatStanceState;
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// Rotate manually if performing action, else rotate with path finding(navMesh)
        /// </summary>
        private void HandleRotateToTarget(EnemyManager enemyManager)
        {
            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = enemyManager.currentTargetStatus.transform.position - enemyManager.transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = enemyManager.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotateSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 relativeDirection = enemyManager.transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemeyRigidbody.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTargetStatus.transform.position);
                enemyManager.enemeyRigidbody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotateSpeed / Time.deltaTime);
            }
        }

    }
}