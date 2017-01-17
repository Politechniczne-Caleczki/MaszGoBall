using Assets.Scripts.GameEngine.Units;
using Assets.Scripts.GUI;
using Assets.Scripts.GUI.Game;
using Assets.Scripts.Network;
using Assets.Scripts.Terrains;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts.States.GameStates
{
    class GameState_AddPlayer:State
    {
        protected override IEnumerator Init()
        {
            if (CustomNetworkManager.AddPlayer()!=null)
            {
                TerrainController tc = GameObject.FindObjectOfType<TerrainController>();

                if(tc!=null)
                    yield return tc.GenerateTerrain(LoadingPanel.OnProgress);
                tc.CreateLight();                
            }

            if (Parent != null)
                Parent.Activate<GameState_Play>();

            yield return base.Init();
        }
    }
}
