using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloGames.Architecture.StateMachine
{
    public class QuequeStateMachine : StateMachine<IState>
    {
        private Type _firstState;
        private Type _lastState;

        public void InitStates(Dictionary<Type, IState> states, Type firstState, Type lastState)
        {
            InitStates(states);
            _firstState = firstState;
            _lastState = lastState;
        }

        public void StartMachine()
        {
            ChangeState(_firstState);
        }

        public void NextState()
        {
            IState targetState = states.Values.SkipWhile(k => k != currentState).Skip(1).DefaultIfEmpty(states[_firstState]).FirstOrDefault();
            ChangeState(targetState);
        }

        public void PrevState()
        {
            IState targetState = states.Values.TakeWhile(k => k != currentState).DefaultIfEmpty(states[_lastState]).LastOrDefault();
            ChangeState(targetState);
        }

        public void Dispose()
        {
            currentState?.Exit();
            currentState = null;
        }
    }

    public class StateMachine<TState> : IStateMachine where TState : IState
    {
        protected Dictionary<Type, TState> states;

        protected TState currentState;

        public event Action<string> OnStateChange;

        public Type CurrentState => currentState.GetType();

        public void InitStates(Dictionary<Type, TState> states)
        {
            this.states = states;
        }

        public void ChangeState(Type type)
        {
            currentState?.Exit();
            currentState = states[type];

            OnStateChange?.Invoke(type.ToString());
            currentState?.Enter();
        }

        protected void ChangeState(TState state)
        {
            currentState?.Exit();
            currentState = state;

            OnStateChange?.Invoke(state.GetType().ToString());
            currentState?.Enter();
        }
    }

    public interface IStateMachine
    {
        public void ChangeState(Type type);
    }
}