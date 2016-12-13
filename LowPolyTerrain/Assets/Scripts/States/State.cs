using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.States
{
    abstract class State
    {
        protected State Parent { get; set; }
        private State Active { get; set; }
        private List<State> Child { get; set; }
        protected State()
        {
            Child = new List<State>();
        }
        protected State(State parent)
        {
            Parent = parent;
            Child = new List<State>();
        }
        protected internal IEnumerator Activate<T>() where T : State
        {
            if (Active != null)
                yield return Active.End();

            Active = Child.Find(x => x.GetType() == typeof(T));
            if (Active == null) throw new Exception("State not found");

            yield return Active.Init();
        }
        protected internal IEnumerator Deactivate<T>() where T : State
        {
            if (Active != null)
                yield return Active.End();

            Active = null;
        }
        protected virtual IEnumerator Init()
        {
            yield return null;
        }
        protected virtual IEnumerator End()
        {
            if (Active != null)
            {
                yield return Active.End();
                Active = null;
                if(Parent !=null)
                    yield return Parent.Init();
            }
        }
        protected virtual void Update()
        {
            if (Active != null)
                Active.Update();
        }
        protected void AddState<T>()where T:  State, new()
        {
            if(Child.Find(x => x.GetType() == typeof(T))!=null) throw new Exception("State arledy exists");
            Child.Add(new T());
        }
        protected void AddState<T>(State parent) where T : State, new()
        {
            if (Child.Find(x => x.GetType() == typeof(T)) != null) throw new Exception("State arledy exists");
            T state = new T();
            state.Parent = parent;
            
            Child.Add(state);
        }
    }
}
