using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private float moveSpeed = 0.3f;

    public GameObject laser;

    private bool isOn = false;
    private bool isPlayed = false;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void On()
    {
        if (isOn)
            return;

        if (!isPlayed)
        {
            audioSource.Play();
            isPlayed = true;    
        }

        //isOn = true;
        Debug.Log("Laser Point On!");
        StartCoroutine(DoorOpen());
    }

    IEnumerator DoorOpen()
    {
        float delta = Time.deltaTime * moveSpeed;

        rightDoor.transform.localPosition += new Vector3(0, 0, delta);
        leftDoor.transform.localPosition -= new Vector3(0, 0, delta);
        yield return null;

        if (leftDoor.transform.localPosition.z <= -3.15f)
        {
            isOn = true;
            laser.SetActive(false);
        }
    }
}
