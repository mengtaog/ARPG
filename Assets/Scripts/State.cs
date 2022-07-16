using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EnemyManager enemyManager, EnemyStatus enemyStatus, EnemyAnimatorHandler enemyAnimatorHandler);

    }
}