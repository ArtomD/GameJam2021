using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private bool shiftDown;
    private bool turnRight;
    private bool turnedRight;
    private bool turnLeft;
    private bool turnedLeft;
    private float rightTimer;
    private float leftTimer;

    private float oneStep = 15;
    private float continuousStep = 3;
    private float shiftStep = 90;

    [SerializeField]
    private CinemachineVirtualCamera camera;
    private float cameraRotation;
    private bool cameraRotated;

    private float gravityStrenght;

    // Start is called before the first frame update
    void Start()
    {
        turnLeft = false;
        turnRight = false;
        turnedRight = false;
        turnedLeft = false;
        cameraRotation = 0;
        cameraRotated = false;
        gravityStrenght = 9.8f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftDown = false;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            shiftDown = true;
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            shiftDown = false;
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            turnRight = true;
            rightTimer = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            turnRight = false;
            turnedRight = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            turnLeft = true;
            leftTimer = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            turnLeft = false;
            turnedLeft = false;
        }
    }

    void FixedUpdate()
    {
        if (turnRight)
        {
            if (rightTimer + 0.2f < Time.time && !shiftDown)
            {
                turnCamera(continuousStep, false);
            }
            else if (!turnedRight)
            {
                if (shiftDown)
                {
                    turnCamera(shiftStep, true);
                    roundRoationTo90();
                }
                else
                {
                    turnCamera(oneStep, false);
                }
                turnedRight = true;
            }

        }
        if (turnLeft)
        {
            if (leftTimer + 0.2f < Time.time && !shiftDown)
            {
                turnCamera(-continuousStep, false);
            }
            else if (!turnedLeft)
            {
                if (shiftDown)
                {
                    turnCamera(-shiftStep, true);
                    roundRoationTo90();
                }
                else
                {
                    turnCamera(-oneStep, false);
                }
                turnedLeft = true;
            }
        }
        if (cameraRotated)
        {
            synchCameraAndGravity();
        }
    }

    private void turnCamera(float amount, bool round)
    {
        cameraRotation += amount;
        if (cameraRotation > 359)
        {
            cameraRotation = cameraRotation - 360;
        }
        else if (cameraRotation < 0)
        {
            cameraRotation = 360 + cameraRotation;
        }
        if (round)
        {
            roundRoationTo90();
        }
        cameraRotated = true;
    }

    private void roundRoationTo90()
    {
        if (cameraRotation >= 270)
        {
            cameraRotation = 270;
        }
        else if (cameraRotation >= 180)
        {
            cameraRotation = 180;
        }
        else if (cameraRotation >= 90)
        {
            cameraRotation = 90;
        }
        else if (cameraRotation >= 0)
        {
            cameraRotation = 0;
        }
        synchCameraAndGravity();
    }

    private void synchCameraAndGravity()
    {
        Vector3 downDirection = -camera.transform.up * gravityStrenght;
        Debug.Log(downDirection);
        Physics2D.gravity = downDirection;
        camera.transform.eulerAngles = new Vector3(0, 0, cameraRotation);
        //camera.gameObject.transform.eulerAngles = new Vector3(0, 0, cameraRotation);
        cameraRotated = false;
    }
}