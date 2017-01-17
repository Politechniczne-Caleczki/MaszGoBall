using Assets.Scripts.GUI;
using Assets.Scripts.States.GameStates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.States
{
    class GameState:State
    {
        public GameState()
        {
            AddState<GameState_CreateServer>(this);
            AddState<GameState_AddPlayer>(this);
        }

        protected override IEnumerator Init()
        {
            yield return SceneManager.LoadSceneAsync(1);
            GUIContainer.GameMenu.CreateServer.onClick.AddListener(Activate<GameState_CreateServer>);
            GUIContainer.GameMenu.Connect.onClick.AddListener(Activate<GameState_AddPlayer>);
            GUIContainer.GameMenu.Exit.onClick.AddListener(Application.Quit);
            base.Init();
        }
    }
}
