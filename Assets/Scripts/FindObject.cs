using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindObject : MonoBehaviour
{
    private void Start()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Treasure" && other.GetComponentInChildren<FruitTreasure>() && transform.parent.GetComponent<PIKMINMovement>().enabled == false)
        {
            
            FruitTreasure ft = other.GetComponentInChildren<FruitTreasure>();

            if (ft.full == false)
            {
                GetComponentInParent<CharacterController>().enabled = false;
                GetComponentInParent<NavMeshAgent>().enabled = true;
                GetComponentInParent<NavMeshAgent>().stoppingDistance = 0;
                GetComponentInParent<NavMeshAgent>().SetDestination(other.transform.position);
            }
            else
            {
                //GetComponentInParent<NavMeshAgent>().isStopped = true;
                //GetComponentInParent<NavMeshAgent>().enabled = false;
            }

            
        }

    }
}
