using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{
    public GameObject whistle;
    public HitUI hitUI;
     AudioSource whistleAudioSource;
    public AudioClip whistleSound;
    float size = 0.1f;
    public float whistleTime = 3f;
    float time = 0f;
    bool play = false;
    // Start is called before the first frame update
    void Start()
    {
        whistleAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && hitUI.hitUI == false && time < whistleTime)
        {
            time += Time.deltaTime;
            if (play == false)
            {
                whistleAudioSource.Stop();
                whistleAudioSource.clip = whistleSound;
                whistleAudioSource.Play();
                play = true;
            }
            

            whistle.SetActive(true);
            whistle.transform.localScale = new Vector3(size, size, size);

            if (size < 10)
            {
                size += Time.deltaTime * 5;
            }

        }
        else
        {
            time = 0;
            whistleAudioSource.Stop();
            play = false;
            size = 0.1f;
            whistle.transform.localScale = new Vector3(size, size, size);
            whistle.SetActive(false);
        }
    }
}
