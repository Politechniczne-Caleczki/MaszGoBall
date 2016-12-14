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
        private Color color;

        [SerializeField]
        private int power;

        [SerializeField]
        public Rigidbody Rigidbody;

        [SerializeField]
        public BoxCollider Collider;


        public Color Color { get { return color; } protected set { color = value; } }

        public string Name { get { return name; } protected set { name = value; } }
        public int Power { get { return power; } private set { power = value; } }

        public void AddPower(int power)
        {
            Power += power;
            Debug.Log("Add power: " + Power);
        }

        public override bool Equals(object obj)
        {
            if (obj is Nation)
                return (obj as Nation).Color == this.Color;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 8)
            {
                 collision.gameObject.GetComponent<Player>().Touch(this);
                //Explosion();
            }
        }

        public void Explosion()
        {
            gameObject.SetActive(false);
            //throw new NotImplementedException();
        }

        public virtual void Catch()
        {
            enabled = false;
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

        public static bool operator ==(Nation n1, Nation n2)
        {
            if (ReferenceEquals(n1, n2))
                return true;

            if ((object)n1 == null || (object)n2 == null)
                return false; 

            return n1.Color == n2.Color;
        }

        public static bool operator !=(Nation n1, Nation n2)
        {
            if (!ReferenceEquals(n1, n2))
                return true;

            if ((object)n1 == null || (object)n2 == null)
                return false;

            return n1.Color != n2.Color;
        }
    }
}
