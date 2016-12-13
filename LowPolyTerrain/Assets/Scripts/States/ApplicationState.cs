using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.States
{
    class ApplicationState: State
    {
        private State Active { get; set; }
        public ApplicationState()
        {
            AddState<MenuState>(this);
            AddState<GameState>(this);
        }

        protected internal IEnumerator Enable()
        {
            yield return Activate<MenuState>();
        }

        protected internal void _Update()
        {
            Update();
        }
    }
}
