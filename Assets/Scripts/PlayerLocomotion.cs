using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [Header("Movement Status")]
        public float walkSpeed = 3f;
        public float runSpeed = 5f;
        public float rotateSpeed = 5f;
        public float fallingSpeed = 1f;



        public Vector3 moveDirection;
        public Transform myTransform;

        [Header("Ground Detect")]
        public float groundDetectionRayStartHeight = 0.5f;
        public int layerForGroundDetect = ~(1 << 8);
        private bool isGrounded;
        private float inAirTimer;

        private InputHandler inputHandler;
        private AnimationHandler animationHandler;
        private PlayerManager playerManager;
        private Transform cameraTransform;
        private new Rigidbody rigidbody;
        private Vector3 normalVector;
        

        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            rigidbody = GetComponent<Rigidbody>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            playerManager = GetComponent<PlayerManager>();
            cameraTransform = Camera.main.transform;
        }

       

        public void HandleMovement()
        {
            
            moveDirection = inputHandler.vertical * cameraTransform.forward;
            moveDirection += inputHandler.horizontal * cameraTransform.right;
            moveDirection.y = 0;
            moveDirection.Normalize();

            
            if (inputHandler.sprintFlag)
            {
                moveDirection *= runSpeed;
            }
            else
            {
                moveDirection *= walkSpeed;
            }
            Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            
            rigidbody.velocity = projectVelocity;
            
            animationHandler.UpdateMovementSpeed(inputHandler.moveAmount);
        }

        public void HandleRotation()
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraTransform.forward * inputHandler.vertical;
            targetDir += cameraTransform.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
            {
                targetDir = myTransform.forward;
            }

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rotateSpeed * Time.deltaTime);
            myTransform.rotation = targetRotation;
        }

        public void HandleRoll()
        {
            if (inputHandler.rollFlag)
            {
                animationHandler.SetTrigger("Roll");
            }
        }

        public void HandleFalling()
        {
            Vector3 origin = transform.position;
            origin.y += groundDetectionRayStartHeight;
            RaycastHit hit;
            if (Physics.Raycast(origin, -Vector3.up, out hit, 1.5f * groundDetectionRayStartHeight, layerForGroundDetect))
            {
                isGrounded = true;
                animationHandler.SetBool("IsGrounded", true);
                inAirTimer = 0;
            }
            else
            {
                inAirTimer += Time.deltaTime;
                rigidbody.AddForce(moveDirection * fallingSpeed / 10f, ForceMode.Acceleration);
                rigidbody.AddForce(-Vector3.up * fallingSpeed, ForceMode.Acceleration);
                if (inAirTimer > 0.2f)
                {
                    isGrounded = false;
                    animationHandler.SetBool("IsGrounded", false);
                }
            }
        }
    }
}