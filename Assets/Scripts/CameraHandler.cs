using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class CameraHandler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraPivotTransform;
        public Transform cameraTransform;
        public Transform defaultPosition;
        public InputHandler inputHandler;

        [Header("Camera Status")]
        public float cameraSphereRadius = 0.1f;
        public float cameraCollisionOffset = 0.1f;
        public float minimumCameraDistance = 0.05f;
        public float lookSpeed;
        public float pivotSpeed;
        public float minimumPivot;
        public float maximumPivot;
        private float lookAngle;
        private float pivotAngle;
        private LayerMask ignoreLayers;


        private Vector3 cameraCurrentVelocity;
        private float defaultCameraDistance;


        private void Start()
        {
            inputHandler = GameObject.Find("Player").GetComponent<InputHandler>();
            
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
            defaultPosition.localPosition = cameraTransform.localPosition;
            defaultCameraDistance = defaultPosition.transform.localPosition.magnitude;
        }

        public void HandleCameraPosition()
        {
            transform.position = targetTransform.position;
            HandleCameraCollision();
        }

        public void HandleCameraRotation()
        {
            lookAngle += (inputHandler.mouseX * lookSpeed) / Time.fixedDeltaTime;
            pivotAngle -= (inputHandler.mouseY * pivotSpeed) / Time.fixedDeltaTime;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCameraCollision()
        {
            Vector3 targetPosition = defaultPosition.transform.position;
            RaycastHit hit;
            //Vector3 direction = defaultCameraPosition;
            Vector3 direction = targetPosition - cameraPivotTransform.position;
           
            if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(direction.magnitude), ignoreLayers))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);

                float t = (dis - cameraCollisionOffset) / defaultCameraDistance;
                if (t < minimumCameraDistance)
                {
                    t = minimumCameraDistance;
                }
                targetPosition = Vector3.Lerp(cameraPivotTransform.position, targetPosition, t);
            }
            cameraTransform.position = targetPosition;
        }
    }
}
