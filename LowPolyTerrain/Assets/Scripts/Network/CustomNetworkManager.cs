using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Network
{
    class CustomNetworkManager : NetworkManager
    {
        private static CustomNetworkManager instance;

        private void Awake()
        {
            instance = this;
        }

        public static NetworkClient RunServer()
        {
            return instance.StartHost();
        }

        public static NetworkClient AddPlayer()
        {
            return instance.StartClient();
        }





    }
}
