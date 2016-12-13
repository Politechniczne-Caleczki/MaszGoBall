using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Terrains;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    class GameStateInit:State
    {
        protected override IEnumerator Init()
        {
            TerrainController terrain = GameObject.FindObjectOfType<TerrainController>();

            yield return terrain.GenerateMesh();
            terrain.RecalculateCollider();




            yield return base.Init();
        }
    }
}
