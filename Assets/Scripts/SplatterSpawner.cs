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
    [SerializeField]
    private GameObject map;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastSpawn = 0;
        delay = 0;
        timer = 0;
        isDead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(rb.velocity.magnitude);
        if (!isDead && rb.velocity.magnitude > 1)
        {
            if (lastSpawn + delay < Time.time)
            {
                lastSpawn = Time.time;
                delay = 0.3f / rb.velocity.magnitude;
                float offsetX = (Random.value - 0.5f) * 0.65f;
                float OffsetY = (Random.value - 0.5f) * 0.65f;
                SplatterController splatterInstance = (SplatterController)Instantiate(splatter, new Vector3(transform.position.x + offsetX, transform.position.y + OffsetY, transform.position.z), transform.rotation);
                splatterInstance.spawnSprite(rb.velocity.magnitude);
                splatterInstance.transform.parent = map.transform;
            }
        }
    }

    public void deathSplatter()
    {
        isDead = true;
        for (int i =0;i<15;i++) {
            float offsetX = (Random.value - 0.5f) * 4f;
            float OffsetY = (Random.value - 0.5f) * 4f;
            SplatterController splatterInstance = (SplatterController)Instantiate(splatter, new Vector3(transform.position.x + offsetX, transform.position.y + OffsetY, transform.position.z), transform.rotation);
            splatterInstance.spawnSprite(15);
            splatterInstance.transform.parent = map.transform;
        }
    }

    public void winSplatter()
    {
        isDead = true;
        for (int i = 0; i < 25; i++)
        {
            float offsetX = (Random.value - 0.5f) * 7f;
            float OffsetY = (Random.value - 0.5f) * 7f;
            SplatterController splatterInstance = (SplatterController)Instantiate(splatter, new Vector3(transform.position.x + offsetX, transform.position.y + OffsetY, transform.position.z), transform.rotation);
            splatterInstance.spawnSprite(18);
            splatterInstance.transform.parent = map.transform;
        }
    }
}
