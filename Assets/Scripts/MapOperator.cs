using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Jam
{
    public class MapOperator : MonoBehaviourPunCallbacks, IPunObservable
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

        [SerializeField]
        private GameObject map;
        [SerializeField]
        private CinemachineVirtualCamera camera;
        private float time = 0;

        [SerializeField] float cameraTurnSpeed = 1.0f;
        private float lastRotate = 0;
        [SerializeField]
        private BallOperator player;

        [SerializeField]
        private float refreshRate = 0.017f;

        private float cameraRotation;
        private bool cameraRotated;

        private float gravityStrenght;

        private Transform pivotPoint;

        void Awake()
        {
            photonView = gameObject.GetComponent<PhotonView>();
            camera = FindObjectOfType<CinemachineVirtualCamera>();
            player = FindObjectOfType<BallOperator>();
            map = GameObject.Find("/Map");
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
            gravityStrenght = 9.8f;
            pivotPoint = camera.transform;
            time = Time.deltaTime;
        }


        void Update()
        {

            if (photonView != null && photonView.IsMine)
            {
                CheckInput();
            }

        }

        void CheckInput()
        {
            time = time + Time.deltaTime;
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

            FU();
        }

        void FU()
        {
            if (turnRight)
            {
                if (shiftDown)
                {

                    if (!turnedRight)
                    {
                        shiftTurn(true);
                        turnedRight = true;
                    }

                }
                else
                {
                    turnMap(cameraTurnSpeed);

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
                    turnMap(-cameraTurnSpeed);

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

            map.transform.RotateAround(camera.transform.position, Vector3.forward, cameraRotation * Time.deltaTime);
            cameraRotated = false;
        }

        private void turnMap(float angle)
        {
            map.transform.RotateAround(camera.transform.position, Vector3.forward, angle * Time.deltaTime);

        }

        private void shiftTurn(bool clockwise)
        {
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
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(map.transform.position);
                stream.SendNext(map.transform.rotation);
            }
            else
            {
                // Network player, receive data
                Vector3 position = (Vector3)stream.ReceiveNext();
                Quaternion rotation = (Quaternion)stream.ReceiveNext();

                map.transform.position = position;
                map.transform.rotation = rotation;


            }
        }
    }
}