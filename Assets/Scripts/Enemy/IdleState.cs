using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class IdleState : State
    {
        public PursueTargetState pursueTargetState;
        public LayerMask detectionLayerMask;

        public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStatus, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //look for a target
            //switich to puesue State if find a target
            #region Handle enemy target detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayerMask);
            foreach (Collider collider in colliders)
            {
                CharacterStatus characterStatus = collider.transform.GetComponent<CharacterStatus>();
                if (characterStatus != null)
                {
                    //Check for team_id

                    Vector3 targetDirection = characterStatus.transform.position - transform.position;
                    float viewAngle = Vector3.Angle(transform.forward, targetDirection);

                    if (viewAngle < enemyManager.maxViewAngle && viewAngle > enemyManager.minViewAngle)
                    {
                        enemyManager.currentTargetStatus = characterStatus;
                        break;
                    }
                }
            }
            #endregion

            #region Handle switching to next state

            if (enemyManager.currentTargetStatus != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion


            return this;
        }
    }
}