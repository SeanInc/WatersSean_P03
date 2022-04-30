using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhistleCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PIKMIN" && other.GetComponent<PIKMINMovement>().enabled == false)
        {
            other.transform.parent = null;
            other.GetComponentInChildren<FindObject>().gameObject.GetComponent<SphereCollider>().enabled = true;
            other.GetComponent<Collider>().enabled = true;
            other.GetComponent<PIKMINMovement>().enabled = true;
            other.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
