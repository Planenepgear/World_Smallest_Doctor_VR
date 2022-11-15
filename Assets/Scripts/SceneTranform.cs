using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class SceneTranform : MonoBehaviour
{
    public GameObject refObject;
    public Transform locator;
    public Transform playerCamera;
    public Transform characterModle;

    public float turnSpeed = 5;
    public float moveSpeed = 20;

    public float minDistanceHorizon = 5;
    public float maxDistanceHorizon = 15;

    public float maxDistanceVertical = 10;
    public float minDistanceVertical = -10;

    //public InputActionProperty LJoystick;
    public InputActionProperty RJoystick;
    public InputActionProperty BForfard;
    public InputActionProperty ABack;

    private Vector2 joystickValue;
    private Vector2 standVector = new(1, 0);

    private void Start()
    {
        //this.transform.position = playerCamera.position + playerCamera.InverseTransformPoint(new Vector3(0, -10, 50));
        Invoke(nameof(FirstPos), 0.2f);
    }

    void Update()
    {
        if (refObject.activeInHierarchy == true)
        {
            joystickValue = RJoystick.action.ReadValue<Vector2>();
            float angle = DotToAngle(standVector, joystickValue);

            if (joystickValue.x > 0.1f || joystickValue.y > 0.1f || joystickValue.x < -0.1f || joystickValue.y < -0.1f)
            {
                //Debug.Log(angle);

                if (angle >= 50 && angle <= 130 && joystickValue.y > 0)
                {
                    //if(this.transform.position.y - playerCamera.position.y > 0 && this.transform.position.y - playerCamera.position.y < maxDistanceVertical)
                    //if(this.transform.position.y - playerCamera.position.y < maxDistanceVertical)
                    if (characterModle.position.y - playerCamera.position.y < maxDistanceVertical)
                    {
                        this.transform.Translate(moveSpeed * Time.deltaTime * Vector3.up, Space.World);
                    }
                }
                else if (angle >= 50 && angle <= 130 && joystickValue.y < 0)
                {
                    //if(this.transform.position.y - playerCamera.position.y <= 0 && this.transform.position.y - playerCamera.position.y > minDistanceVertical)
                    //if(this.transform.position.y - playerCamera.position.y > minDistanceVertical)
                    if (characterModle.position.y - playerCamera.position.y > minDistanceVertical)
                    {
                        this.transform.Translate(moveSpeed * Time.deltaTime * Vector3.down, Space.World);
                    }
                }
                else if (joystickValue.x > 0)
                {
                    this.transform.Rotate(Vector3.up, Time.deltaTime * -turnSpeed, Space.World);
                }
                else if (joystickValue.x < 0)
                {
                    this.transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed, Space.World);
                }
            }

            if (BForfard.action.ReadValue<float>() == 1 && Vector3.Distance(this.transform.position, new Vector3(locator.position.x, this.transform.position.y, locator.position.z)) < maxDistanceHorizon)
            {
                this.transform.Translate(moveSpeed * Time.deltaTime * locator.forward, Space.World);
            }
            else if (ABack.action.ReadValue<float>() == 1 && Vector3.Distance(this.transform.position, new Vector3(locator.position.x, this.transform.position.y, locator.position.z)) > minDistanceHorizon)
            {
                this.transform.Translate(-moveSpeed * Time.deltaTime * locator.forward, Space.World);
            }
        }
    }

    public float DotToAngle(Vector3 _from, Vector3 _to)
    {
        float rad = Mathf.Acos(Vector3.Dot(_from.normalized, _to.normalized));
        return rad * Mathf.Rad2Deg;
    }

    public void FirstPos()
    {
        this.transform.position = playerCamera.position + new Vector3(0, -7 + characterModle.position.y, 10);
    }
}
