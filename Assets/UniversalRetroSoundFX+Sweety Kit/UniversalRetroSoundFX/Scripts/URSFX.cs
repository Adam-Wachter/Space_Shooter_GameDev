using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URSFX : MonoBehaviour
{

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.pitch = (Random.Range(0.5f, 1.5f)); // Random pitch
        audioSource.Play();
    }
}
