using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class PlayerManager : MonoBehaviour
    {
        public InputHandler inputHandler;
        public PlayerLocomotion playerLocomotion;
        public BattleManager battleManager;
        public AnimationHandler animationHandler;
        public CameraHandler cameraHandler;

        [Header("Player Status")]
        public bool isInteracting;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            battleManager = GetComponent<BattleManager>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            cameraHandler = GameObject.Find("Main Camera").GetComponentInParent<CameraHandler>();
        }

        private void Update()
        {
            inputHandler.TickInput();

            UpdatePlayerStatus();

            if (isInteracting == false)
            {
                playerLocomotion.HandleMovement();
                playerLocomotion.HandleRotation();
                playerLocomotion.HandleRoll();
            }

            battleManager.HandleAttack();
            battleManager.HandleDefense();
            playerLocomotion.HandleFalling();
            
        }

        private void LateUpdate()
        {
            cameraHandler.HandleCameraPosition();
            cameraHandler.HandleCameraRotation();
        }

        private void UpdatePlayerStatus()
        {
            isInteracting = animationHandler.GetBool("IsInteracting");
        }
    }
}
