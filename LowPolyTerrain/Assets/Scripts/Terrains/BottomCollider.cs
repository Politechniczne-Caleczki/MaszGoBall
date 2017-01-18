using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Terrains
{
    [RequireComponent(typeof(BoxCollider))]
    class BottomCollider:MonoBehaviour
    {

        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.transform.position += new Vector3(0, 2, 0);
        }

        private void OnCollisionStay(Collision collision)
        {
            collision.gameObject.transform.position += new Vector3(0, 2, 0);
        }

    }
}
