using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Creatures;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    class CustomNetworkManager : NetworkManager
    {
        private CreatureManager creatureManager;

        private bool canRotate = true;

        private float rotation;
        
        public override NetworkClient StartHost()
        {
            canRotate = false;
            var client = base.StartHost();
            creatureManager = GameObject.FindObjectOfType<CreatureManager>();
            creatureManager.CmdCreate();

            return client;
        }

        public override void OnStopHost()
        {
            creatureManager.CmdDestroy();
            canRotate = true;
            base.OnStopHost();
        }

        void Update()
        {
            if (!canRotate)
                return;

            rotation += 3 * Time.deltaTime;
            if (rotation >= 360f)
                rotation -= 360f;

            Camera.main.transform.position = Vector3.zero;
            Camera.main.transform.rotation = Quaternion.Euler(0, rotation, 0);
            Camera.main.transform.Translate(0, 5, -5);
            Camera.main.transform.LookAt(Vector3.zero);
        }
    }
}
