using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackSound : MonoBehaviour
{
    public AudioClip attSound; // przypisz PotionPickup.wav
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            audioSource.PlayOneShot(attSound);
        }
    }
}
