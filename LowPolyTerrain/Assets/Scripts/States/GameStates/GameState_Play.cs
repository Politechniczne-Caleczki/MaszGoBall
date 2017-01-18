using Assets.Scripts.GameEngine.Units;
using Assets.Scripts.GUI;
using Assets.Scripts.GUI.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    class GameState_Play:State
    {
        private Player Player { get; set; }


        protected override IEnumerator Init()
        {
            GUIContainer.LoadingPanel.gameObject.SetActive(false);
            GUIContainer.GameMenu.gameObject.SetActive(false);

            GameObject.FindObjectOfType<Assets.Scripts.GameEngine.Units.Camera>().enabled = true;

            Player = GameObject.FindObjectOfType<Player>();
            Player.enabled = true;

            GUIContainer.GameGUI.gameObject.SetActive(true);


            return base.Init();
        }

        protected override void Update()
        {
            GameGUI.Compass.transform.eulerAngles= new Vector3 (0, 0, Player.transform.eulerAngles.y);

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                GameGUI.ShowScorePanel();
            }else if(Input.GetKeyUp(KeyCode.Tab))
            {
                GameGUI.HideScorePanel();
            }

            base.Update();
        }
    }
}
