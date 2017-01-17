using Assets.Scripts.GUI.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    class GUIContainer:MonoBehaviour
    {
        private static GUIContainer instance;

        [SerializeField]
        private LoadingPanel loadingPanel;

        [SerializeField]
        private GameMenu gameMenu;

        [SerializeField]
        private GameGUI gameGUI;

        private void Awake()
        {
            instance = this;
        }

        public static GameGUI GameGUI
        {
            get
            {
                return instance.gameGUI;
            }
        }

        public static LoadingPanel LoadingPanel
        {
            get
            {
                return instance.loadingPanel;
            }
        }

        public static GameMenu GameMenu
        {
            get
            {
                return instance.gameMenu;
            }
        }

    }
}
