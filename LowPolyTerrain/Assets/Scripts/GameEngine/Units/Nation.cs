using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.GameEngine.Units
{
    public class Nation: NetworkBehaviour
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
                        if(CanCath)
                            collision.gameObject.GetComponent<Player>().Touch(this);
                    }
                    break;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            switch (collision.gameObject.layer)
            {
                case 8:
                case 9:
                    {
                        if (!CanCath)
                        {
                            CanCath = true;
                            Debug.Log("CanCath");
                        }
                    }
                    break;
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
