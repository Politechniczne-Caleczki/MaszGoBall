using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameEngine.Units
{
    class Spiral:Creature
    {

        public void Jump()
        {
            Rigidbody.AddForce(Quaternion.Euler(transform.eulerAngles)* new Vector3(0, 1.5f, 0.50f), ForceMode.Impulse);
        }
    }
}
