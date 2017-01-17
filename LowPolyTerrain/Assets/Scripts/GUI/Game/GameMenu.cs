using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    class GameMenu: MonoBehaviour
    {
        [SerializeField]
        private Button connect;

        [SerializeField]
        private Button createServer;

        [SerializeField]
        private Button exit;


        public Button Connect
        {
            get { return connect; }
        }

        public Button CreateServer
        {
            get { return createServer; }
        }

        public Button Exit
        {
            get { return exit; }
        }

    }
}
