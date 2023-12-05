using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;


public class NavScript : MonoBehaviour
{
    public Transform target;
    public Transform bed;
    
    private NavMeshAgent pet;
    private Animator petAnim;

    private LayerMask foodLayer;

    [SerializeField] private float FoodScanRange = 4;
    
    // Start is called before the first frame update
    void Awake()
    {
        pet= GetComponent<NavMeshAgent>();
        petAnim = GetComponent<Animator>();
        StartIdle();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, FoodScanRange);
    }

    void ScanFood()
    {
        Collider[] colls = Physics.OverlapSphere(this.transform.position, FoodScanRange);

        foreach (Collider col in colls)
        {
            if (col.gameObject.CompareTag("Draggable"))
            {
                target = col.gameObject.transform;
                StartChase();
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Draggable"))
        {
            StartEat();
        }
    }

    void StartChase()
    {
        pet.SetDestination(target.position);
        petAnim.SetBool("isWalk", true);
        petAnim.SetBool("isIdle", false);
        petAnim.SetBool("doEat", false);
    }

    void StartIdle()
    {
        pet.SetDestination((bed.position));
        petAnim.SetBool("isWalk", false);
        petAnim.SetBool("isIdle", true);
        petAnim.SetBool("doEat", false);
    }

    void StartEat()
    {
        target = pet.transform;
        petAnim.SetBool("isWalk", false);
        petAnim.SetBool("isIdle", false);
        petAnim.SetBool("doEat", true);
        Invoke("StartIdle", 2);
    }
    
    void Update()
    {
        ScanFood();
        
        if (target == null)
        {
            target = pet.transform;
        }
    }
}
