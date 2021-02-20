using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterSpawner : MonoBehaviour
{
    private Rigidbody2D rb;
    public SplatterController splatter;
    private float timer;
    private float lastSpawn;
    private float delay;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastSpawn = 0;
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 1)
        {
            if (lastSpawn + delay < Time.time)
            {
                lastSpawn = Time.time;
                delay = 0.3f / rb.velocity.magnitude;
                float offsetX = (Random.value - 0.5f) * 0.5f;
                float OffsetY = (Random.value - 0.5f) * 0.5f;
                SplatterController splatterInstance = (SplatterController)Instantiate(splatter, new Vector3(offsetX, OffsetY, transform.position.z), transform.rotation);
                splatterInstance.spawnSprite(rb.velocity.magnitude);
            }
        }
    }
}
