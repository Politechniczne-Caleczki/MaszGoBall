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

        private Nation ChatchNation { get; set; }

        

        private void Start()
        {
            animator.SetInteger("State", 0);
        }


        public void Catch(Nation nation)
        {
            animator.SetInteger("State", 1);
            ChatchNation = nation;
            //nation.transform.position = Vector3.zero;
        }

        public void Update()
        {
            if (ChatchNation != null)
            {
                ChatchNation.transform.position = Handle.position;
                ChatchNation.transform.eulerAngles = Handle.eulerAngles;
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
            if (ChatchNation)
            {
                ChatchNation.Rigidbody.AddForce(Quaternion.Euler(0, transform.parent.transform.eulerAngles.y, 0) * new Vector3(0, 1.5f, 1), ForceMode.Impulse);
                ChatchNation.Shot();
                ChatchNation = null;
            }
        }


    }
}
