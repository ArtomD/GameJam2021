using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Jam
{
    public class WinObject : MonoBehaviour
    {
        private LevelController levelController;

        // Start is called before the first frame update
        void Start()
        {
            levelController = FindObjectOfType<LevelController>();
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
                player.Win();
                levelController.Win();
            }
        }
    }
}