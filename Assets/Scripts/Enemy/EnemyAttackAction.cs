using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MT
{
    [CreateAssetMenu(menuName = "AI/EnemyAction/EnemyAttackAction")]
    public class EnemyAttackAction : EnemyAction
    {
        public float damage;
        public float recoveryTime;

        public float minAttackDistance;
        public float maxAttackDistance;

        public float minAttackAngle;
        public float maxAttackAngle;
    }
}