using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameEngine.Units
{
    class Tentakel: MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private Transform Handle;

        private Nation Nation { get; set; }

        public Action OnEndShot { get; set; }

        public bool IsReady
        {
            get
            {
                return Nation == null;
            }
        }

        private void Start()
        {
            animator.SetInteger("State", 0);
            Nation = null;
        }

        public void Catch(Nation nation)
        {
            if (Nation == null)
            {
                animator.SetInteger("State", 1);
                Nation = nation;
            }
            else throw new Exception("Tantakel not free");
        }

        public void Update()
        {
            if (Nation != null)
            {
                Nation.transform.position = Handle.position;
                Nation.transform.eulerAngles = Handle.eulerAngles;
            }
        }

        public IEnumerator Rotate(Vector3 position)
        {
            yield return null;
        }

        public void Shot()
        {
            animator.SetInteger("State", 2);
        }


        public void EndShot()
        {
            if (Nation)
            {
                Nation.Rigidbody.AddForce(Quaternion.Euler(0, transform.parent.transform.eulerAngles.y, 0) * new Vector3(0, 1.5f, 1), ForceMode.Impulse);
                Nation.Shot();
                Nation = null;
                OnEndShot();
            }
        }


    }
}
