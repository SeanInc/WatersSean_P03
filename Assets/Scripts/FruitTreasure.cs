using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;


public class FruitTreasure : MonoBehaviour
{
    public float money = 100f;
    public float weightMAX = 1.0f;
    public float amountConnected = 0.0f;
    public List<GameObject> grabSpots;
    public List<bool> hasPikmin;
    public List<GameObject> connectedPikmin;
    GameObject grab;
    SphereCollider sphereCollider;
    NavMeshAgent treasureAgent;
    public bool isCarried = false;
    bool lift = false;
    public bool full = false;
    public TextMeshPro textPrefab;
    TextMeshPro text;
    public float speedMultiplier = 1;
    Vector3 addScale;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        grab = new GameObject();

        text = Instantiate(textPrefab);
        text.name = transform.parent.name + ("Text");
        addScale = new Vector3(0, transform.parent.Find("Art").GetChild(0).localScale.y, 0);
        //Debug.Log(addScale);
        text.transform.position = transform.position + new Vector3(0, 1f, 0) + addScale;
        //Debug.Log(text.transform.position);

        treasureAgent = GetComponentInParent<NavMeshAgent>();

        SetGrabPoints();
        DisableArtColliders();

    }

    private void Update()
    {
        Quaternion temp = Quaternion.LookRotation(Camera.main.transform.position - text.transform.position, Vector3.up);
        Quaternion offset = Quaternion.Euler(0f, -180f, 0f);
        text.transform.rotation = temp * offset;
        //text.transform.rotation = Quaternion.Euler(0, temp.y - 90, 0);
        text.transform.position = transform.position + new Vector3(0, 1f, 0) + addScale;

        if (amountConnected >= weightMAX / 2)
        {
            
            float speed = (float)(amountConnected / (weightMAX / 2));
            treasureAgent.speed = speedMultiplier * speed;
            //Debug.Log(speed);
        }


        if (amountConnected >= weightMAX)
        {
            full = true;
        }    
    }

    // Update is called once per frame
    void SetGrabPoints()
    {
        int i = 0;
        int rotation = 0;
        GameObject temp;
        while (i < weightMAX)
        {
            temp = Instantiate(grab, transform);
            temp.name = "grab" + i;
            grabSpots.Add(temp);
            hasPikmin.Add(false);
            temp.transform.parent = transform;
            temp.transform.localRotation = Quaternion.AngleAxis(rotation, Vector3.up);
            rotation += 360 / (int)weightMAX;
            temp.transform.localPosition = temp.transform.TransformDirection(new Vector3(sphereCollider.radius - 0.15f, 0, 0));
            //Debug.Log(temp.name + ": rotation: " + temp.transform.rotation + " position: " + temp.transform.position);
            i++;
        }

    }

    void DisableArtColliders()
    {
        foreach (Transform child in transform.parent.Find("Art"))
        {
            if (child.GetComponent<Collider>())
            {
                child.GetComponent<Collider>().enabled = false;
                Debug.Log("Disabled collider for " + child.name);
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PIKMINMovement pMovement) && other.GetComponentInChildren<FindObject>().enabled == true)
        {
            //Debug.Log("Found Pikmin");
            if ((pMovement.enabled == false && other.GetComponentInChildren<FindObject>().enabled == true) && full == false)
            {
                //Debug.Log("Pikmin Connected");
                other.transform.parent = transform.parent;
                // other.GetComponentInParent<SphereCollider>().enabled = false;
                other.GetComponentInChildren<GroundCheck>().enabled = false;
                other.GetComponent<CharacterController>().enabled = false;
                other.GetComponent<Rigidbody>().useGravity = false;
                //other.GetComponent<NavMeshAgent>().ResetPath();
                other.GetComponent<NavMeshAgent>().enabled = false;
                other.GetComponentInChildren<FindObject>().enabled = false;

                other.GetComponentInChildren<FindObject>().gameObject.GetComponent<SphereCollider>().enabled = false;
                other.GetComponent<CharacterController>().enabled = false;

                int i = 0;
                bool foundSpot = false;
                while (i < grabSpots.Count && foundSpot == false)
                {
                    if (hasPikmin[i] == false)
                    {
                        other.transform.position = grabSpots[i].transform.position;
                        hasPikmin[i] = true;
                        foundSpot = true;
                    }
                    i++;
                }
                


                connectedPikmin.Add(other.gameObject);
                amountConnected = connectedPikmin.Count;
            }
            
            
        }

        if (amountConnected >= weightMAX / 2)
        {
            

            if (lift == false)
            {
                treasureAgent.enabled = true;
                isCarried = true;
                transform.parent.Find("Art").position += new Vector3(0, 0.15f, 0);
                lift = true;
            }
            

            if (FindObjectOfType<Onion>() && isCarried == true)
            {
                treasureAgent.SetDestination(FindObjectOfType<Onion>().transform.position);
                
            }
        }
        else
        {
            if (lift == true)
            {
                if (isCarried == false)
                {
                    treasureAgent.Stop();
                }
                
                treasureAgent.enabled = false;
                isCarried = false;
                transform.parent.Find("Art").position -= new Vector3(0, 0.15f, 0);
                Debug.Log("Drop");
                lift = false;
            }



                
        }

        Text();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PIKMIN")
        {
            
            hasPikmin[connectedPikmin.IndexOf(other.gameObject)] = false;
            Debug.Log(hasPikmin[connectedPikmin.IndexOf(other.gameObject)]);
            connectedPikmin.Remove(other.gameObject);
            amountConnected = connectedPikmin.Count;
        }
    }
    void Text()
    {
        if (amountConnected < 1)
        {
            text.text = "";
        }
        if (amountConnected >= 1 && amountConnected <= weightMAX/2)
        {
            text.text = amountConnected + "\n - \n" + weightMAX/2;
            text.color = Color.white;
        }
        if (amountConnected >= weightMAX / 2)
        {
            text.text = amountConnected + "\n - \n" + weightMAX / 2;
            text.color = Color.red;
        }
    }
}
