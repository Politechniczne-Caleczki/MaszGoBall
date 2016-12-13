using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameEngine.Units
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player: MonoBehaviour
    {
        [SerializeField]
        private Rigidbody Rigidbody;


        public string Name { get; private set; }
        public Nation Nation { get; private set; }
        private bool CanJump { get;  set; }

        public void Touch(Nation nation)
        {
            if (Nation == null)
                AddNation(nation);
            else
                CheckNation(nation);
        }
        private void CheckNation(Nation nation)
        {
            if (Nation == nation)
                Nation.AddPower(nation.Power);
            else
                KillPlayer(nation);
        }
        private void KillPlayer(Nation nation)
        {
            if (nation is Bomb)
            {
                Debug.Log("Zabity przez gracza");
            }else
            {
                Debug.Log("Zabity przez moba");
            }
            Nation = null;
        }
        private void AddNation(Nation nation)
        {
            Nation = nation;
            Debug.Log("Set nation" + Nation.name);
        }
        private void Start()
        {
            gameObject.layer = 8;
            CanJump = true;
        }
        private void Update()
        { 
            Vector3 position = transform.position;
            transform.position = position + (Quaternion.Euler(transform.eulerAngles) * new Vector3(Input.GetAxis("Horizontal") / 500, 0, Input.GetAxis("Vertical")/350));
            
            if(Input.GetAxis("Jump")!=0)
                Rigidbody.AddForce(new Vector3(0,0.1f, 0), ForceMode.Impulse);

            if (Input.GetAxis("Fire2") != 0)
            {
                Vector3 euler = transform.eulerAngles;
                euler.y += Input.GetAxis("Rotate");
                transform.eulerAngles = euler;
            }

            Jump();
        }

        private void Jump()
        {
            if (CanJump)
                if (Input.GetAxis("Jump") != 0)
                {
                    Rigidbody.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
                    CanJump = false;
                }
        }

        private void OnCollisionEnter(Collision collision)
        {            
            if(!CanJump && collision.gameObject.layer == 13)
                CanJump = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer == 13)
                CanJump = false;
        }

    }
}
