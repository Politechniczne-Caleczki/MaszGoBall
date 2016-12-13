using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameEngine.Units
{

    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Player: MonoBehaviour
    {
        public string Name { get; private set; }
        public Nation Nation { get; private set; }
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
        }

        private void Update()
        { 
            Vector3 position = transform.position;
            position.z +=   Input.GetAxis("Vertical")/100;
            transform.position = position;
        }

    }
}
