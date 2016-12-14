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


        private void Start()
        {
            animator.SetInteger("State", 0);
        }


        public void Catch(Vector3 position)
        {
            animator.SetInteger("State", 1);
        }

        public IEnumerator Rotate(Vector3 position)
        {


            yield return null;
        }



        public void Shot()
        {
            animator.SetInteger("State", 2);
        }


    }
}
