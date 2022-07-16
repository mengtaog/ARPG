using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtNearBy : MonoBehaviour
{
    public Transform headTransform;
    public Transform aimTargetTransform;
    public float sightRadius;
    public float lerpSpeed;

    public Vector3 originLocalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originLocalPosition = aimTargetTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(headTransform.position + headTransform.forward * sightRadius, sightRadius);
        
        Vector3 targetPosition = Vector3.zero;
        foreach (var collider in colliders)
        {

            if (collider.GetComponent<PointOfInterst>())
            {
                targetPosition = collider.GetComponent<PointOfInterst>().GetInterstTransfrom().position;
                Debug.Log("In" + targetPosition);
                break;
            }
        }

        

        if (targetPosition == Vector3.zero)
        {
            targetPosition = transform.position + originLocalPosition;
        }

        aimTargetTransform.position = Vector3.Lerp(aimTargetTransform.position, targetPosition, Time.deltaTime * lerpSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green * 0.5f;
        Gizmos.DrawSphere(headTransform.position + headTransform.forward*sightRadius, sightRadius);
    }
}
