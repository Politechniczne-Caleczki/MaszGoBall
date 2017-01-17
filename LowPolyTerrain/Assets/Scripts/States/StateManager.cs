using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.States
{
    class StateManager: MonoBehaviour
    {
        private static ApplicationState AppState;

        public static StateManager Current { get; private set; }

        private void Awake()
        {
            if (!Current)
            {
                Current = this;
                DontDestroyOnLoad(Current);
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0);
            AppState = new ApplicationState();
            yield return AppState.Enable();
        }

        private void Update()
        {
            if(AppState!=null)
                AppState._Update();
        }

        public static void StartCorutine(IEnumerator e)
        {
            Current.StartCoroutine(e);
        }
    }
}
