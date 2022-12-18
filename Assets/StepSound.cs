using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    private CharacterController characterController;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController.velocity.sqrMagnitude < 0.5f)
        {
            audioSource.Stop();
            return;
        }
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
