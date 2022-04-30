using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Onion : MonoBehaviour
{
    public float suckTime = 2.0f;
    public float theTime = 0f;
    float colorTime = 0.00f;

    public GameObject glowSpot;
    public GameObject onion;

    public AudioSource audioSource;
    public AudioClip chaching;

    Score score;

    public void Start()
    {
        Time.timeScale = 1;
        score = GameObject.FindObjectOfType<Score>();
    }

    private void Update()
    {
        OnionColor();
    }

    void OnionColor()
    {
        colorTime += Time.deltaTime/10;
        onion.GetComponent<Renderer>().material.color = Color.HSVToRGB(colorTime, 1, 1);
        if (colorTime >= 1)
        {
            colorTime = 0;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.tag);
        if (other.transform.tag == "Treasure" && other.GetComponentInChildren<FruitTreasure>())
        {
            //Debug.Log("Found treasre");
            if (other.GetComponentInChildren<FruitTreasure>().isCarried == true)
            {
                //GameObject fruitData = other.transform.Find("FruitData").gameObject;
                //Debug.Log("In here");
                int i = 0;
                while (i < other.GetComponentInChildren<FruitTreasure>().amountConnected)
                {
                    foreach (Transform child in other.transform)
                    {
                        //Debug.Log("Remove Pikmin");
                        if (child.tag == "PIKMIN")
                        {
                            child.transform.parent = null;
                            child.GetComponentInChildren<FindObject>().gameObject.GetComponent<SphereCollider>().enabled = true;
                            child.GetComponent<Collider>().enabled = true;
                            child.GetComponent<PIKMINMovement>().enabled = true;
                            child.GetComponent<NavMeshAgent>().enabled = true;
                        }
                        
                        


                    }
                    /*
                    if (other.transform.Find("PIKMIN" + i))
                    {
                        Debug.Log("Remove Pikmin " + i);
                        Transform temp = other.transform.Find("PIKMIN" + i);
                        temp.transform.parent = null;
                        temp.GetComponentInChildren<FindObject>().gameObject.GetComponent<SphereCollider>().enabled = true;
                        temp.GetComponent<PIKMINMovement>().enabled = true;
                        temp.GetComponent<NavMeshAgent>().enabled = true;
                        
                    }
                    */

                    i++;
                }
                other.GetComponentInChildren<FruitTreasure>().amountConnected = 0;
                other.GetComponent<NavMeshAgent>().enabled = false;
                StartCoroutine(SuckItem(other.gameObject));
            }
        }

    }

    IEnumerator SuckItem(GameObject treasure)
    {
        treasure.GetComponent<NavMeshAgent>().enabled = false;
        treasure.GetComponentInChildren<FruitTreasure>().isCarried = false;
        treasure.GetComponentInChildren<FruitTreasure>().enabled = false;
        Transform art = treasure.transform.Find("Art");

        treasure.GetComponentInChildren<FruitTreasure>().connectedPikmin.Clear();
        treasure.GetComponentInChildren<FruitTreasure>().hasPikmin.Clear();
        treasure.GetComponentInChildren<FruitTreasure>().grabSpots.Clear();

        yield return new WaitForSeconds(0.5f);
        

        theTime = 0;
        

        

        Vector3 artPosition = art.position;
        Vector3 artScale = art.localScale;
        while (theTime <= suckTime)
        {
            //Debug.Log(art.position);
            art.position = Vector3.Lerp(artPosition, glowSpot.transform.position, theTime/suckTime);
            art.localScale = Vector3.Lerp(artScale, new Vector3(0, 0, 0), theTime / suckTime);
            theTime += Time.deltaTime;
            //Debug.Log(theTime);
            yield return null;
        }

        audioSource.clip = chaching;
        audioSource.Play();

        score.addPoints(treasure.GetComponentInChildren<FruitTreasure>().money);

        Destroy(GameObject.Find(art.parent.name + "Text"));
        Destroy(treasure);
        
        yield return null;
    }
}
