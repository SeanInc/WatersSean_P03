using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundCheck : MonoBehaviour
{
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded)
        {
            //GetComponentInParent<Collider>().enabled = true;
            GetComponentInParent<CharacterController>().enabled = false;
            GetComponentInParent<NavMeshAgent>().enabled = true;
        }
        {
            //GetComponentInParent<CharacterController>().enabled = true;
        }
    }

    
}
