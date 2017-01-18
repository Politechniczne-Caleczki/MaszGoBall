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

        [SyncVar]
        private bool Enabled = true;
        public NationType NationType { get { return nationType; } protected set { nationType = value; } }
        public string Name { get { return name; } protected set { name = value; } }
        public bool CanCath { get; private set; }
        private void Start()
        {
            CanCath = true;
            Enabled = true;
        }
        protected virtual void OnCollisionEnter(Collision collision)
        {
            switch(collision.gameObject.layer)
            {
                case 8:
                    {
                        if(CanCath)
                            collision.gameObject.GetComponent<Player>().CmdTouch(this.gameObject);
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
                        }
                    }
                    break;
            }
        }
        public void Explosion()
        {
            gameObject.SetActive(false);
        }


        public virtual void CmdCatch()
        {
            Enabled = false;

            gameObject.GetComponent<Animator>().enabled = false;
            CanCath = enabled = false;
            Collider.enabled = false;
            Rigidbody.useGravity = false;

        }


        public virtual void CmdShot()
        {
            Enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
            enabled = true;
            Collider.enabled = true;
            Rigidbody.useGravity = true;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        protected virtual void Update()
        {

            if (!Enabled)
                CmdCatch();
            else
                CmdShot();
        }


    }
}
