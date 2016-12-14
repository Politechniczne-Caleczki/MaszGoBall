using System;
using System.Collections;
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
        [SerializeField]
        private Tentakel Tentakel;
        [SerializeField]
        private Animator animator;

        public string Name { get; private set; }
        public Nation Nation { get; private set; }
        private bool CanJump { get;  set; }
        Vector3 NewPosition { get; set; }

        public void Touch(Nation nation)
        {
            if (Nation == null)
                AddNation(nation);
            else
                CheckNation(nation);
        }
        private void CheckNation(Nation nation)
        {
          //  if (Nation != nation)
                //Nation.AddPower(nation.Power);
            //else
            //    KillPlayer(nation);
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
            nation.Explosion();
            Nation = null;
        }
        private void AddNation(Nation nation)
        {

            Nation = nation;
            Nation.Catch();
            Tentakel.Catch(nation);
        }
        private void Shot()
        {
            Tentakel.Shot();
            Nation = null;
        }
        private void Start()
        {
            gameObject.layer = 8;
            CanJump = true;
        }

        private void FixedUpdate()
        {
            transform.position = NewPosition;
        }

        private void Update()
        { 
            NewPosition = transform.position;

            NewPosition += (Quaternion.Euler(transform.eulerAngles) * new Vector3(Input.GetAxis("Horizontal") / 500, 0, Input.GetAxis("Vertical")/100));

            if (Input.GetAxis("Vertical") != 0)
            {
                animator.SetBool("Run", true);
                if (animator.GetInteger("State") != 1)
                    animator.SetInteger("State", 2);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetInteger("State", 0);
            }


            if (Input.GetAxis("Fire2") != 0)
            {
                Vector3 euler = transform.eulerAngles;
                euler.y += Input.GetAxis("Rotate");
                transform.eulerAngles = euler;
            }

            if (Nation != null)
            {
                if (Input.GetAxis("Fire1") != 0)
                    Shot();
            }

            Jump();
        }

        private void Jump()
        {
            if (CanJump)
            {
                if (Input.GetAxis("Jump") != 0)
                {
                    Rigidbody.AddForce(new Vector3(0, 20, 0), ForceMode.Acceleration);
                    animator.SetInteger("State", 1);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            switch(collision.gameObject.layer)
            {
                case 13:
                    {
                        if(!CanJump)
                            StartCoroutine(JumpDelay());
                    }
                    break;
            }
        }

        private IEnumerator JumpDelay()
        {
            if (!CanJump)
            {
                yield return new WaitForSeconds(0.1f);
                CanJump = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            switch (collision.gameObject.layer)
            {
                case 13:
                    {
                        CanJump = false;
                    }
                    break;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            switch (collision.gameObject.layer)
            {
                case 13:
                    {
                        Jump();
                    }
                    break;
            }
        }


        public void EndJump()
        {
            if(animator.GetBool("Run"))
                animator.SetInteger("State", 2);
            else
                animator.SetInteger("State", 0);

        }
    }
}
