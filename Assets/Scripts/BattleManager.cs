using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class BattleManager : MonoBehaviour
    {
        private InputHandler inputHandler;
        private AnimationHandler animationHandler;
        private PlayerManager playerManager;



        public void HandleAttack()
        {
            if (inputHandler.lightAttackFlag)
            {
                animationHandler.SetTrigger("LightAttack");
            }
        }

        public void HandleDefense()
        {
            if (inputHandler.defenseFlag)
            {
                animationHandler.SetBool("IsDefensing", true);
            }
            else
            {
                animationHandler.SetBool("IsDefensing", false);
            }
        }


        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            playerManager = GetComponent<PlayerManager>();
        }
    }
}