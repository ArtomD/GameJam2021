using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Jam
{
    public class BallOperator : MonoBehaviour
    {
        public PhotonView photonView;
        [SerializeField] float rollSpeed = 0.1f;
        [SerializeField] float jumpVelocity = 0.1f;

        [SerializeField] float jumpMemoryThreshold = 0.2f;        

        [SerializeField] float fHorizontalDampingWhenStopping = 0.2f;
        [SerializeField] float fHorizontalDampingWhenTurning = 0.2f;
        [SerializeField] float fHorizontalDampingBasic = 0.2f;
        [SerializeField] float groundedMemoryThreshold = 0.1f;

        float yVelocityTracker = 0.0f;
        float timeSinceGrounded = 0.0f;
        
        float groundedRayLength = 0.1f;

        [SerializeField] float health = 1.0f;

        [SerializeField]
        private CinemachineVirtualCamera camera;

        float jumpKeyDownMemory = 0;
        float groundedMemory = 0;

        Rigidbody2D rigidBody;
        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Jump();
            Roll();

            maintainVelocity();
        }

        private void maintainVelocity()
        {            
            if (isGrounded(groundedRayLength))            
                timeSinceGrounded = 0;            
            else            
                timeSinceGrounded += Time.deltaTime;
            
            if (yVelocityTracker < rigidBody.velocity.y)
            {                
                if(timeSinceGrounded > groundedMemoryThreshold)                                    
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocityTracker);
                else
                {
                    yVelocityTracker = rigidBody.velocity.y;
                }
            }
            else            
                yVelocityTracker = rigidBody.velocity.y;           
            
        }
        private void Roll()
        {
            float thrust = Input.GetAxis("Horizontal") * rollSpeed;

            if (thrust != 0)
            {                
                    rigidBody.velocity = new Vector2(thrust, rigidBody.velocity.y);         
            }




        }

        private void Jump()
        {
            
            jumpKeyDownMemory -= Time.deltaTime;
            groundedMemory -= Time.deltaTime;

            

            if (Input.GetButtonDown("Jump"))
                jumpKeyDownMemory = jumpMemoryThreshold;



            
            
            if (this.isGrounded(groundedRayLength))
            {
                groundedMemory = groundedMemoryThreshold;
            }


            if (jumpKeyDownMemory > 0 && groundedMemory > 0)
            {
                //Vector3 thrustVector = (camera.transform.up * jumpVelocity);
                //rigidBody.velocity = Physics2D.gravity.normalized * (Vector2.Dot(rigidBody.velocity, Physics2D.gravity) / Physics2D.gravity.magnitude) + new Vector2(thrustVector.x, thrustVector.y);
                Debug.Log(camera.transform.up);
                //rigidBody.velocity = camera.transform.TransformDirection(new Vector2(camera.transform.TransformDirection(rigidBody.velocity).x, camera.transform.TransformDirection(0.0f, jumpVelocity, 0.0f).y));

                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVelocity);
                //camera.transform.TransformDirection(new Vector2(rigidBody.velocity.x, jumpVelocity));

            }
        }

        public void Hurt(float damage)
        {
            this.health = this.health - damage;
            if (this.health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {

        }

        private bool isGrounded(float length)
        {                        
            RaycastHit2D raycast = Physics2D.Raycast(gameObject.GetComponent<CircleCollider2D>().bounds.center, -camera.gameObject.transform.up, gameObject.GetComponent<CircleCollider2D>().bounds.extents.y + length, LayerMask.GetMask("Foreground"));
            Color color;
            
            if(raycast.collider != null)            
                color = Color.green;            
            else            
                color = Color.red;
            
            Debug.DrawRay(gameObject.GetComponent<CircleCollider2D>().bounds.center, - camera.gameObject.transform.up * (gameObject.GetComponent<CircleCollider2D>().bounds.extents.y + length), color);
            return raycast.collider != null;            
        }
    }
}