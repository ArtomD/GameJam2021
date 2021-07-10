using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Jam
{
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
            BallOperator player = otherObject.gameObject.GetComponent<BallOperator>();
            if (player != null)
            {
                player.Hurt(impactDamage);
            }
        }
    }

}
