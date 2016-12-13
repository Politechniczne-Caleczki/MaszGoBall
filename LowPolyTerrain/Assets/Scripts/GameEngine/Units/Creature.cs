using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameEngine.Units
{
    public class Creature: Nation
    {
        private void Start()
        {
            gameObject.layer = 9;
        }
    }
}
