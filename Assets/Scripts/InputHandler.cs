using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class InputHandler : MonoBehaviour
    {
        public float vertical;
        public float horizontal;
        public float mouseX;
        public float mouseY;
        public float moveAmount;

        [Header("GamePlay Buttons")]
        public bool rollButton;
        


        [Header("GamePlay Flags")]
        public bool rollFlag;
        public bool sprintFlag;
        public bool lightAttackFlag;
        public bool defenseFlag;

        private Vector2 movementInput;
        private Vector2 deltaMouseInput;
        private float rollTimer;

        PlayerControls inputActions;

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += inputActions => deltaMouseInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerActions.Roll.started += inputActions => rollButton = true;
                inputActions.PlayerActions.Roll.canceled += inputActions => rollButton = false;
                inputActions.PlayerActions.LightAttack.started += inputActions => lightAttackFlag = true;
                inputActions.PlayerActions.Defense.started += inputActions => defenseFlag = true;
                inputActions.PlayerActions.Defense.canceled += inputActions => defenseFlag = false;

            }
            inputActions.Enable();
        }

        public void TickInput()
        {
            HandleLocomotionInput();
            HandleRollAndSprintInput();
            //TestInput();
        }

        private void HandleDefenseInput()
        {

        }

        private void HandleLocomotionInput()
        {
            vertical = movementInput.y;
            horizontal = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            mouseX = deltaMouseInput.x;
            mouseY = deltaMouseInput.y;
        }

        private void HandleRollAndSprintInput()
        {
            if (rollButton)
            {
                rollTimer += Time.deltaTime;
                sprintFlag = true;
                rollFlag = false;
            }
            else
            {
                if (rollTimer > 0 && rollTimer < 0.5f)
                {
                    rollFlag = true;
                }
                else
                {
                    rollFlag = false;
                }
                sprintFlag = false;
                rollTimer = 0;
            }
        }

        private void LateUpdate()
        {
            lightAttackFlag = false;
        }

        private void TestInput()
        {
            /*
            if (inputActions.Test.Test.phase == UnityEngine.InputSystem.InputActionPhase.Started)
            {
                Debug.Log("test start");
            }
            else if (inputActions.Test.Test.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
            {
                Debug.Log("test perform");
            }
            else if (inputActions.Test.Test.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
            {
                Debug.Log("test cancel");
            }
            */
            if (rollButton) Debug.Log("rollButton == true");
            else Debug.Log("rollButton == false");
        }
    }
}