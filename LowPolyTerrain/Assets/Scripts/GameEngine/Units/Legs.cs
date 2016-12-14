using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameEngine.Units
{
    class Legs: Creature
    {
        private void Step()
        {
            Rigidbody.AddForce(Quaternion.Euler(transform.eulerAngles) * new Vector3(0, 0.5f, 1.5f), ForceMode.Impulse);
        }
    }
}
