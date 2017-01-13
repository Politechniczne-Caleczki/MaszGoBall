using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Terrains;
using UnityEngine;
using Assets.Scripts.GUI.Game;

namespace Assets.Scripts.States.GameStates
{
    class GameStateInit:State
    {
        protected override IEnumerator Init()
        {
            TerrainController terrain = GameObject.FindObjectOfType<TerrainController>();

            yield return terrain.GenerateTerrain(LoadingPanel.OnProgress);
            yield return base.Init();
        }

        protected override IEnumerator End()
        {
            LoadingPanel.Disable();
            return base.End();
        }
    }
}
