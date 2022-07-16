using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator animator;

        public void UpdateAnimatorParameters(string name, float value)
        {
            animator.SetFloat(name, value);
        }

        public void SetTrigger(string name)
        {
            animator.SetTrigger(name);
        }

        public void ResetTrigger(string triggerName)
        {
            animator.ResetTrigger(triggerName);
        }

        public void SetBool(string name, bool value)
        {
            animator.SetBool(name, value);
        }

        public bool GetBool(string name)
        {
            return animator.GetBool(name);
        }

        public void SetFloat(string name, float value)
        {
            animator.SetFloat(name, value);
        }

        public void SetFloat(string name, float value, float dampTime, float deltaTime)
        {
            animator.SetFloat(name, value, dampTime, deltaTime);
        }

        public void PlayTargetAnimation(string name)
        {
            animator.Play(name);
        }

        public void PlayTargetAnimation(string name, bool isInteracting)
        {
            animator.Play(name);
            animator.SetBool("IsInteracting", isInteracting);
        }
    }
}
