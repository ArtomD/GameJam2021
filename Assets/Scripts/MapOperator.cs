using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOperator : MonoBehaviour
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
    private GameObject map;
    [SerializeField]
    private CinemachineVirtualCamera camera;
    [SerializeField]
    private GameObject player;
    private float cameraRotation;
    private bool cameraRotated;

    private float gravityStrenght;

    private Transform pivotPoint;

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
        pivotPoint = camera.transform;
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

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            pivotPoint = camera.transform;
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            pivotPoint = player.transform;
        }
    }

    void FixedUpdate()
    {
        if (turnRight)
        {
            if (shiftDown)
            {

                if (!turnedRight) {
                    shiftTurn(true);
                    turnedRight = true;
                }

            }
            else
            {
                turnMap(100f);
            }
        }
        
        if (turnLeft)
        {
            if (shiftDown)
            {

                if (!turnedLeft)
                {
                    shiftTurn(false);
                    turnedLeft = true;
                }

            }
            else
            {
                turnMap(-100f);
            }
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

        //Physics2D.gravity = downDirection;
        map.transform.RotateAround(camera.transform.position, Vector3.forward, cameraRotation* Time.deltaTime);
        //map.transform.eulerAngles = new Vector3(0, 0, cameraRotation);
        //player.transform.eulerAngles = new Vector3(0, 0, cameraRotation);
        //camera.gameObject.transform.eulerAngles = new Vector3(0, 0, cameraRotation);
        cameraRotated = false;
    }

    private void turnMap(float angle)
    {
        map.transform.RotateAround(camera.transform.position, Vector3.forward, angle * Time.deltaTime);
    }

    private void shiftTurn(bool clockwise)
    {
        Debug.Log(map.transform.localEulerAngles);
        if (clockwise)
        {
            map.transform.RotateAround(pivotPoint.position, Vector3.forward, 90 - (map.transform.localEulerAngles.z % 90));
        }
        else
        {
            if (map.transform.localEulerAngles.z % 90 == 0)
            {
                map.transform.RotateAround(pivotPoint.position, Vector3.forward, -90);
            }
            else
            {
                map.transform.RotateAround(pivotPoint.position, Vector3.forward, -(map.transform.localEulerAngles.z % 90));
            }
        }
        
    }
}