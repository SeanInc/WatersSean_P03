using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class ThrowPikmin : MonoBehaviour
{
    public List<GameObject> Squad;
    Cursor cursor;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public float flightDuration = 0;
    public HitUI hitUI;
    public AudioSource audioSource;
    public AudioClip throwSound;

    public TextMeshProUGUI squadText;
    // Start is called before the first frame update
    void Start()
    {
        cursor = FindObjectOfType<Cursor>().GetComponent<Cursor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Squad.Count > 0)
        {
            if (Input.GetButtonDown("Fire1") && hitUI.hitUI == false)
            {
                audioSource.Stop();
                audioSource.clip = throwSound;
                audioSource.Play();
                //Debug.Log(Squad[0].name);
                //Squad[0].GetComponent<PIKMINMovement>().followPlayer = false;
                Squad[0].GetComponent<PIKMINMovement>().enabled = false;
                Squad[0].GetComponent<NavMeshAgent>().enabled = false;
                StartCoroutine(Throw(Squad[0]));

            }
        }
        squadText.text = "Squad: " + Squad.Count;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PIKMINMovement>())
        {
            Squad.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PIKMINMovement>())
        {
            Squad.Remove(other.gameObject);
        }
    }

    IEnumerator Throw(GameObject pikmin)
    {

        pikmin.transform.position = this.GetComponentInParent<Transform>().position + new Vector3(0, 2.0f, 0);

        float target_Distance = Vector3.Distance(pikmin.transform.position, cursor.transform.position);

        //Debug.Log("Cursor location: " + cursor.transform.position + "    pikmin location: " + pikmin.transform.position + "   Distance: " + target_Distance);

        float projectile_Velocity = target_Distance / (Mathf.Sin( firingAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        flightDuration = target_Distance / Vx;

        pikmin.transform.rotation = Quaternion.LookRotation(cursor.transform.position - pikmin.transform.position);

        float elapse_time = 0;

        //Debug.Log(flightDuration);

        Debug.Log("FD: " + flightDuration);

        while (elapse_time < flightDuration)
        {
            pikmin.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            elapse_time += Time.deltaTime;

            yield return null;
        }
        
        if (elapse_time >= flightDuration || pikmin.GetComponentInChildren<GroundCheck>().isGrounded)
        {
            pikmin.GetComponentInChildren<FindObject>().enabled = true;
            pikmin.GetComponent<CharacterController>().enabled = true;
            //pikmin.GetComponent<NavMeshAgent>().enabled = true;
            //  pikmin.GetComponent<NavMeshAgent>().enabled = true;
            //Debug.Log("Landed");
        }

        /*
        float t = 0;
        Vector3 cursorPosition = cursor.cursorLocation;
        Vector3 pikminPosition = pikmin.transform.position;

        Debug.Log(pikmin.transform.position + " to " +cursorPosition);

        pikmin.GetComponent<PIKMINMovement>().Toss();
        while (t < 1)
        {
            Debug.Log(t);
            t += Time.deltaTime / 2f;
            pikmin.transform.position = Vector3.Lerp(pikminPosition, cursorPosition, t);
            yield return null;
        }    

            
        */
        
    }
}
