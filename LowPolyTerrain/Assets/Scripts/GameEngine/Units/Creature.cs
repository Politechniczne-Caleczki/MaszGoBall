using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameEngine.Units
{
    public class Creature: Nation
    {

        [SerializeField]
        private Animator animator;

        private void Start()
        {
            gameObject.layer = 9;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.layer)
            {
                case 8:
                case 14:
                    {
                        Rigidbody.MoveRotation(Quaternion.Euler(new Vector3(transform.eulerAngles.x, UnityEngine.Random.Range(0, 360f), transform.eulerAngles.z)));
                    }
                    break;
            }
            base.OnCollisionEnter(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            switch (collision.gameObject.layer)
            {
                case 8:
                case 14:
                    {
                        Rigidbody.MoveRotation(Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 5, transform.eulerAngles.z)));
                    }
                    break;

            }
        }

        public override void Catch()
        {
            animator.SetInteger("State", 0);

            base.Catch();
        }

        public override void Shot()
        {
            animator.SetInteger("State", 1);
            base.Shot();
        }
    }
}
