using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Jam
{
    public class CameraOperator : MonoBehaviourPunCallbacks, IPunObservable
    {
        public PhotonView photonView;

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

        public GameObject camera;
        private float cameraRotation;
        private bool cameraRotated;

        private float gravityStrength;


        private Vector3 downDirection = new Vector3(0, -9.8f, 0);
        void Awake()
        {
            photonView = gameObject.GetComponent<PhotonView>();
            camera = gameObject;
            // camera.enabled = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            turnLeft = false;
            turnRight = false;
            turnedRight = false;
            turnedLeft = false;
            cameraRotation = 0;
            cameraRotated = false;
            gravityStrength = 9.8f;
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView!= null && photonView.IsMine)
            {
                CheckInput();
            }
        }

        void CheckInput()
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
                SyncCameraAndGravity();
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
            SyncCameraAndGravity();
        }

        private void SyncCameraAndGravity()
        {

            camera.transform.eulerAngles = new Vector3(0, 0, cameraRotation);
            //camera.gameObject.transform.eulerAngles = new Vector3(0, 0, cameraRotation);
            cameraRotated = false;
            downDirection = -camera.transform.up * gravityStrength;
            Physics2D.gravity = downDirection;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(downDirection);
            }
            else
            {
                // Network player, receive data
                Vector3 received = (Vector3)stream.ReceiveNext();
                Debug.Log(received);
                Physics2D.gravity = received;
            }
        }


    }
}