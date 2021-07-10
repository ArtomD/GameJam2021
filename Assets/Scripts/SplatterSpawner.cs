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

    [SerializeField]
    private AudioClip spray_clip;
    [SerializeField]
    private AudioClip explode_clip;

    private AudioSource spraySource;
    private AudioSource explodeSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastSpawn = 0;
        delay = 0;
        timer = 0;
        isDead = false;
        map = GameObject.Find("/Map");

        spraySource = Utils.AddAudioNoFalloff(gameObject, spray_clip, true, false, 1, 1);
        explodeSource = Utils.AddAudioNoFalloff(gameObject, explode_clip, false, false, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead && rb.velocity.magnitude > 1)
        {
            if (!spraySource.isPlaying)
            {
                spraySource.Play();
            }
            spraySource.volume = 0.3f + Mathf.Min((rb.velocity.magnitude / 8),0.3f);
            spraySource.pitch = 0.7f + (rb.velocity.magnitude / 10);
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
        else
        {
            if (spraySource.isPlaying)
            {
                spraySource.Stop();
            }
        }
    }

    public void deathSplatter()
    {
        isDead = true;
        for (int i = 0; i < 15; i++)
        {
            float offsetX = (Random.value - 0.5f) * 4f;
            float OffsetY = (Random.value - 0.5f) * 4f;
            SplatterController splatterInstance = (SplatterController)Instantiate(splatter, new Vector3(transform.position.x + offsetX, transform.position.y + OffsetY, transform.position.z), transform.rotation);
            splatterInstance.spawnSprite(15);
            splatterInstance.transform.parent = map.transform;
        }
        explodeSource.Play();
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
