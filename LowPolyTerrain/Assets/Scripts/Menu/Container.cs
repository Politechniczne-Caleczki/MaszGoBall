using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Menu
{
    class Container: MonoBehaviour
    {
        [SerializeField]
        private Button startGameButton;



        public Button StartGameButton { get { return startGameButton; } }

    }
}
