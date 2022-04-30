using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PIKMINMovement : MonoBehaviour
{
    public CharacterController controller;
    public NavMeshAgent agent;
    //You may consider adding a rigid body to the zombie for accurate physics simulation
    private GameObject wayPoint;
    private Vector3 wayPointPos;
    //This will be the zombie's speed. Adjust as necessary.
    private float speed = 6.0f;
    public float gravity = -9.81f;
    public float throwHeight = 3f;
    Vector3 velocity;
    Vector3 moveLocation;
    Renderer renderer;


    GroundCheck groundCheck;

    
    public bool followPlayer = true;

    public bool inRadius = false;
    void Start()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();

        renderer = GetComponentInChildren<Renderer>();
        SetRandomColor();

        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        //At the start of the game, the zombies will find the gameobject called wayPoint.
        wayPoint = GameObject.Find("wayPoint");
    }

    void SetRandomColor()
    {
        renderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
    }    

    void Update()
    {
        wayPoint = GameObject.Find("wayPoint");
        if (GetComponentInChildren<GroundCheck>().isGrounded == true)
        {
            controller.enabled = false;
            GetComponent<NavMeshAgent>().enabled = true;

            if (inRadius == false && followPlayer == true)
            {
                // wayPointPos = new Vector3(wayPoint.transform.position.x, transform.position.y, wayPoint.transform.position.z);
                //Here, the zombie's will follow the waypoint.
                //moveLocation = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
                //moveLocation = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);

                agent.SetDestination(wayPoint.transform.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
        else
        {
            GetComponent<NavMeshAgent>().enabled = false;
            controller.enabled = true;
            controller.Move(velocity * Time.deltaTime);
            
        }


        //controller.Move(velocity * Time.deltaTime);
        Gravity();

    }

    void Gravity()
    {
        
        if (groundCheck.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }




}
