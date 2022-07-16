using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStatus, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //check for attack range
            //walk around player or circle them
            //if in attack range return attack state
            if (enemyManager.distanceFromTarget <= enemyManager.maxAttackRange && enemyManager.recoveryTimer <= 0)
            {
                return attackState;
            }
            else if (enemyManager.distanceFromTarget > enemyManager.maxAttackRange)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
        }
    }
}
