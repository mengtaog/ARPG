using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public string sleepAnimation;
        public string wakeUpAnimation;
        //public float detectionRadius;
        public LayerMask detectionLayerMask;

        public PursueTargetState pursueTargetState;

        public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStatus, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            if (isSleeping && enemyManager.isInteracting == false)
            {
                enemyAnimatorHandler.PlayTargetAnimation(sleepAnimation, true);
            }

            #region Handle target detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayerMask);
            foreach (Collider collider in colliders)
            {
                CharacterStatus characterStatus = collider.transform.GetComponent<CharacterStatus>();
                if (characterStatus != null)
                {
                    Vector3 targetDirection = characterStatus.transform.position - transform.position;
                    float viewAngle = Vector3.Angle(transform.forward, targetDirection);

                    if (viewAngle < enemyManager.maxViewAngle && viewAngle > enemyManager.minViewAngle)
                    {
                        enemyManager.currentTargetStatus = characterStatus;
                        isSleeping = false;
                        enemyAnimatorHandler.PlayTargetAnimation(wakeUpAnimation);
                        break;
                    }
                }
            }
            #endregion

            #region Handle State change
            if (enemyManager.currentTargetStatus != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }

            #endregion

        }
    }
}