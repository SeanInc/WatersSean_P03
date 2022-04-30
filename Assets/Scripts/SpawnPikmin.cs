using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPikmin : MonoBehaviour
{
    public GameObject PikminPrefab;
    public List<AudioClip> pluckSounds;
    public AudioSource audioSource;
    int i = 0;
    // Start is called before the first frame update
    public void SummonPikmin()
    {
        audioSource.Stop();
        Instantiate(PikminPrefab, transform.position, Quaternion.Euler(0,0,0), this.transform).name = "PIKMIN" + i++;

        audioSource.clip = pluckSounds[Random.Range(0, pluckSounds.Count)];
        audioSource.Play();

    }
  
}
