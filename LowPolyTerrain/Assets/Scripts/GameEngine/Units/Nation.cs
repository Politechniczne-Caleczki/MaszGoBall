using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameEngine.Units
{
    public class Nation: MonoBehaviour
    {
        [SerializeField]
        private NationType nationType;
        [SerializeField]
        public Rigidbody Rigidbody;
        [SerializeField]
        public BoxCollider Collider;
        public NationType NationType { get { return nationType; } protected set { nationType = value; } }
        public string Name { get { return name; } protected set { name = value; } }
        public bool CanCath { get; private set; }
        private void Start()
        {
            CanCath = true;
        }
        protected virtual void OnCollisionEnter(Collision collision)
        {
            switch(collision.gameObject.layer)
            {
                case 8:
                    {
                        collision.gameObject.GetComponent<Player>().Touch(this);
                    }
                    break;
                case 13:
                    {
                        if (!CanCath)
                            CanCath = true;
                    }break;
            }
        }
        public void Explosion()
        {
            gameObject.SetActive(false);

        }
        public virtual void Catch()
        {
            CanCath = enabled = false;
            Collider.enabled = false;
            Rigidbody.useGravity = false;
        }
        public virtual void Shot()
        {
            enabled = true;
            Collider.enabled = true;
            Rigidbody.useGravity = true;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}
