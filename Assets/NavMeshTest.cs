using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MT
{
    public class NavMeshTest : MonoBehaviour
    {
        public Transform target;
        private NavMeshAgent nav;
        // Start is called before the first frame update
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            nav.SetDestination(target.position);
        }
    }
}