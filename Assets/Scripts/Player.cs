using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float rollSpeed = 0.1f;
    [SerializeField] float jumpVelocity = 0.1f;

    [SerializeField] float jumpMemoryThreshold = 0.2f;
    [SerializeField] float groundedMemoryThreshold = 0.2f;

    [SerializeField] float fHorizontalDampingWhenStopping = 0.2f;
    [SerializeField] float fHorizontalDampingWhenTurning = 0.2f;
    [SerializeField] float fHorizontalDampingBasic = 0.2f;

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
    }

    private void Roll()
    {        
        float thrust = Input.GetAxis("Horizontal") * rollSpeed;

        if (thrust != 0)
        {
            rigidBody.velocity = new Vector2(thrust, rigidBody.velocity.y);
            if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f)
                thrust *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10.0f);
            else if (Mathf.Abs(Input.GetAxis("Horizontal")) != Mathf.Sign(thrust))
                thrust *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10.0f);
            else
                thrust *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10.0f);
        }
            

        
        
    }

    private void Jump()
    {
        jumpKeyDownMemory -= Time.deltaTime;
        groundedMemory -= Time.deltaTime;              

        if (Input.GetButtonDown("Jump"))        
            jumpKeyDownMemory = jumpMemoryThreshold;            
        
            

        if (rigidBody.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            groundedMemory = groundedMemoryThreshold;            
        }
            

        if (jumpKeyDownMemory > 0 && groundedMemory > 0)
        {
            
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVelocity);
            
        }
    }
}
