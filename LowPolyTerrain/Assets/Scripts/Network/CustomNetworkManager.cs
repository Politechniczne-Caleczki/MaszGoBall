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

        public static bool Active()
        {
            return instance.isNetworkActive;
        }

        public static NetworkClient RunServer()
        {
            return instance.StartHost();
        }


        public static NetworkClient AddPlayer()
        {
            return instance.StartClient();
        }

        public static void Stop()
        {
            instance.StopClient();
            instance.StopHost();
            instance.StopServer();
        }
    }
}
