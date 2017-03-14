using Assets.Scripts.States;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States
{
    class Root: State
    {
        public Root()
        {
            AddState<MenuState>(this);
            AddState<GameState>(this);
        }

        protected internal IEnumerator Enable()
        {
            yield return _Activate<MenuState>();
        }

        protected internal void _Update()
        {
            Update();
        }
    }
}
