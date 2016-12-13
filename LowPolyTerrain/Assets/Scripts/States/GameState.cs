using Assets.Scripts.States.GameStates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.States
{
    class GameState:State
    {
        public GameState()
        {
            AddState<GameStateInit>(this);
        }

        protected override IEnumerator Init()
        {
            yield return SceneManager.LoadSceneAsync(2);

            yield return Activate<GameStateInit>();

            yield return base.Init();
        }

    }
}
