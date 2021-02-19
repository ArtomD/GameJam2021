using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float impactDamage = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void OnTriggerEnter2D(Collider2D otherObject)
    {
        Player player = otherObject.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Hurt(impactDamage);
        }
    }
}
