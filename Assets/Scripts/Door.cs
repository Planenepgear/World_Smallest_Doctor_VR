using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IItem
{
    [SerializeField] private GameObject[] gameObjects;
    public void Input(bool value)
    {
        foreach(var i in gameObjects)
            i.SetActive(value);
    }
}
