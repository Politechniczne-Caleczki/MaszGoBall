using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using Assets.Scripts.Terrains;
using UnityEngine;
using Assets.Scripts.GUI.Game;
using UnityEngine.Networking;

namespace Assets.Scripts.States.GameStates
{
    class GameStateInit:State
    {
        private NetworkManagerHUD hud;

        protected override IEnumerator Init()
        {
            TerrainController terrain = GameObject.FindObjectOfType<TerrainController>();
            hud = GameObject.FindObjectOfType<NetworkManagerHUD>();
            hud.showGUI = false;

            yield return terrain.GenerateTerrain(LoadingPanel.OnProgress);
            yield return base.Init();
        }

        protected override IEnumerator End()
        {
            LoadingPanel.Disable();

            hud.showGUI = true;
            var a = GameObject.FindObjectOfType<CustomNetworkManager>();
            //a.StartHost();
            

            return base.End();
        }
    }
}
