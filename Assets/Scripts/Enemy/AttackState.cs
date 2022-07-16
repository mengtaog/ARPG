using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;
        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;


        public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStatus, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //select one of our many attack
            //set recovery timer
            //return to combat stance state

            if (enemyManager.isPerformingAction) return combatStanceState;



            if (currentAttack != null)
            {
                if (enemyManager.distanceFromTarget < currentAttack.minAttackDistance)
                {
                    return this;
                }
                else if (enemyManager.distanceFromTarget < currentAttack.maxAttackDistance)
                {
                    if (enemyManager.viewAngleToTarget < currentAttack.maxAttackAngle && enemyManager.viewAngleToTarget > currentAttack.minAttackAngle)
                    {
                        enemyAnimatorHandler.SetFloat("MovementSpeed", 0, 0.1f, Time.deltaTime);
                        enemyAnimatorHandler.PlayTargetAnimation(currentAttack.AnimationName, true);
                        enemyManager.isPerformingAction = true;
                        enemyManager.recoveryTimer = currentAttack.recoveryTime;
                        currentAttack = null;
                        return combatStanceState;
                    }
                }
            }
            else
            {
                GetNewCurrentAttack(enemyManager);
            }

            return combatStanceState;
        }


        private void GetNewCurrentAttack(EnemyManager enemyManager)
        {

            List<int> availableAttackIndex = new List<int>();

            if (currentAttack != null) return;

            for (int i = 0; i < enemyAttacks.Length; ++i)
            {
                if (enemyManager.distanceFromTarget > enemyAttacks[i].maxAttackDistance || enemyManager.distanceFromTarget < enemyAttacks[i].minAttackDistance
                    || enemyManager.viewAngleToTarget > enemyAttacks[i].maxAttackAngle || enemyManager.viewAngleToTarget < enemyAttacks[i].minAttackAngle)
                {
                    continue;
                }
                else
                {
                    availableAttackIndex.Add(i);
                }
            }

            if (availableAttackIndex.Count > 0)
            {
                int attackIndex = availableAttackIndex[Random.Range(0, availableAttackIndex.Count)];
                currentAttack = enemyAttacks[attackIndex];
            }
        }
    }
}