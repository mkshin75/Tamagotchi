using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent pet;
    
    // Start is called before the first frame update
    void Start()
    {
        pet= GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            pet.destination = target.position;
        }
    }
}
