using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class AnimationHandler : AnimatorHandler
    {
        private InputHandler inputHandler;
        public new Rigidbody rigidbody;


        public bool enableAnimationMove;

        private void Start()
        {
            animator = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            rigidbody = GetComponentInParent<Rigidbody>();
        }

        

        public void UpdateMovementSpeed(float moveAmount)
        {
            if (inputHandler.sprintFlag)
            {
                animator.SetFloat("MovementSpeed", moveAmount * 2, 0.1f, Time.deltaTime);
            }
            else
            {
                animator.SetFloat("MovementSpeed", moveAmount, 0.1f, Time.deltaTime);
            }

            
        }
        
        private void OnAnimatorMove()
        {
            if (animator.GetBool("IsInteracting") == false) return;

            Vector3 deltaPosition = animator.deltaPosition;
            //deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            rigidbody.velocity = velocity;
        }

    }
}
