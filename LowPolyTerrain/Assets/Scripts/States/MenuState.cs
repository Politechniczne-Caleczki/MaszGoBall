using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Menu;
using System.Collections;

namespace Assets.Scripts.States
{
    class MenuState:State
    {
        protected override IEnumerator Init()
        {
            yield return SceneManager.LoadSceneAsync(1);

            Container container = GameObject.FindObjectOfType<Container>();
            container.StartGameButton.onClick.AddListener(StartGame);

            base.Init();
        }

        private void StartGame()
        {
            StateManager.Current.StartCoroutine(Parent.Activate<GameState>());
        }


    }
}
