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

            yield return terrain.GenerateTerrain(LoadingPanel.OnProgress);

            CreateLight();

            yield return base.Init();
        }

        private void CreateLight()
        {
            GameObject oldLight = GameObject.Find("Directional Light");
            GameObject.Destroy(oldLight);

            GameObject newlight = new GameObject("Directional Light");
            newlight.transform.eulerAngles = new Vector3(70, 10, 0);
            Light light = newlight.AddComponent<Light>();
            light.type = LightType.Directional;

        }

        protected override IEnumerator End()
        {
            LoadingPanel.Disable();     

            yield return base.End();
        }
    }
}
