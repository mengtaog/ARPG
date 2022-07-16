using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MT
{
    public class EnemyManager : MonoBehaviour
    {

        [Header("A.I Settings")]
        public float detectionRadius = 10;
        public float maxViewAngle = 50;
        public float minViewAngle = -50;

        public float rotateSpeed = 15;
        public float maxAttackRange = 1.5f;

        public float distanceFromTarget;
        public float viewAngleToTarget;
        public CharacterStatus currentTargetStatus;
        public NavMeshAgent navMeshAgent;

        public State currentState;
        public bool isPerformingAction;
        public bool isInteracting;
        public float recoveryTimer = 0;
        public Rigidbody enemeyRigidbody;
        private EnemyLocomotion enemyLocomotion;
        private EnemyAnimatorHandler enemyAnimatorHandler;
        private EnemyStatus enemyStatus;

        private void Awake()
        {
            enemyLocomotion = GetComponent<EnemyLocomotion>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            enemyStatus = GetComponent<EnemyStatus>();
            enemeyRigidbody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        }

        private void Start()
        {
            enemyAnimatorHandler.SetBool("IsGrounded", true);
            navMeshAgent.enabled = false;
            enemeyRigidbody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();
            isInteracting = enemyAnimatorHandler.GetBool("IsInteracting");
        }

        private void FixedUpdate()
        {
            HandleDistanceFromTarget();
            HandleStateMachine();
        }

        private void HandleStateMachine()
        {
            if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStatus, enemyAnimatorHandler);
                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }

        }

        private void SwitchToNextState(State nextState)
        {
            currentState = nextState;
        }

        /*
        private void HandleCurrentAction()
        {
            if (enemyLocomotion.currentTargetStatus == null)
            {
                enemyLocomotion.HandleDetection();
            }
            else if (enemyLocomotion.distanceFromTarget > enemyLocomotion.stopDistance)
            {
                enemyLocomotion.HandleMoveToTarget();
            }
            else
            {
                AttackTarget();
            }
        }

        private void AttackTarget()
        {
            if (isPerformingAction) return;

            if (currentAttack == null) GetNewCurrentAttack();

            enemyAnimatorHandler.PlayTargetAnimation(currentAttack.AnimationName, true);
            isPerformingAction = true;
            recoveryTimer = currentAttack.recoveryTime;
        }

        private void GetNewCurrentAttack()
        {
            Vector3 targetDirection = enemyLocomotion.currentTargetStatus.transform.position - transform.position;
            float viewAngle = Vector3.Angle(targetDirection, transform.forward);
            

            List<int> availableAttackIndex = new List<int>();

            if (currentAttack != null) return;

            for (int i = 0; i < enemyAttacks.Length; ++i)
            {
                if (enemyLocomotion.distanceFromTarget > enemyAttacks[i].maxAttackDistance || enemyLocomotion.distanceFromTarget < enemyAttacks[i].minAttackDistance
                    || viewAngle > enemyAttacks[i].maxAttackAngle || viewAngle < enemyAttacks[i].minAttackAngle)
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
        }*/

        private void HandleRecoveryTimer()
        {
            if (recoveryTimer > 0)
            {
                recoveryTimer -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if (recoveryTimer <= 0)
                {
                    isPerformingAction = false;
                    recoveryTimer = 0;
                }
            }
        }
        

        private void HandleDistanceFromTarget()
        {
            if (currentTargetStatus == null) return;

            distanceFromTarget = Vector3.Distance(currentTargetStatus.transform.position, transform.position);
            Vector3 targetDirection = currentTargetStatus.transform.position - transform.position;
            viewAngleToTarget = Vector3.Angle(targetDirection, transform.forward);
        }
        

      
    }
}
