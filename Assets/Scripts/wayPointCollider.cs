using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


        void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PIKMIN")
        {
            //Debug.Log("In here");
            if (other.gameObject.GetComponent<PIKMINMovement>())
            {
                other.gameObject.GetComponent<PIKMINMovement>().inRadius = true;
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PIKMIN")
        {
            if (other.gameObject.GetComponent<PIKMINMovement>())
            {
                other.gameObject.GetComponent<PIKMINMovement>().inRadius = false;
            }
        }
    }
}
