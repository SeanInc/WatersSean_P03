using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundCheck : MonoBehaviour
{
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;
    CharacterController controller;
    public bool followPlayer = true;

    public float gravity = -9.81f;
    public bool inRadius = false;
    // Start is called before the first frame update
    void Start()
    {
        groundCheck = this.transform;
        controller = GetComponentInParent<CharacterController>();
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
        else
        {
            GetComponentInParent<CharacterController>().enabled = true;
            GetComponentInParent<NavMeshAgent>().enabled = false;
        }

        Gravity();

        
        if (isGrounded == false)
        {
            GetComponentInParent<NavMeshAgent>().enabled = false;
            controller.enabled = true;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Gravity()
    {

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }

}
