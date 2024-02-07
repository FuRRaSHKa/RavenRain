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
            IState targetState = States.Values.SkipWhile(k => k != CurrentState).Skip(1).DefaultIfEmpty(States[_firstState]).FirstOrDefault();
            ChangeState(targetState);
        }

        public void PrevState()
        {
            IState targetState = States.Values.TakeWhile(k => k != CurrentState).DefaultIfEmpty(States[_lastState]).LastOrDefault();
            ChangeState(targetState);
        }

        public void Dispose()
        {
            CurrentState?.Exit();
            CurrentState = null;
        }
    }

    public class StateMachine<TState> : IStateMachine where TState : IState
    {
        private Dictionary<Type, TState> _states;
        private TState _currentState;

        public event Action<string> OnStateChange;

        protected Dictionary<Type, TState> States => _states;
        protected TState CurrentState {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
            }
        }

        public Type CurrentStateType => _currentState.GetType();

        public void InitStates(Dictionary<Type, TState> states)
        {
            this._states = states;
        }

        public void ChangeState(Type type)
        {
            _currentState?.Exit();
            _currentState = _states[type];

            OnStateChange?.Invoke(type.ToString());
            _currentState?.Enter();
        }

        protected void ChangeState(TState state)
        {
            _currentState?.Exit();
            _currentState = state;

            OnStateChange?.Invoke(state.GetType().ToString());
            _currentState?.Enter();
        }
    }

    public interface IStateMachine
    {
        public void ChangeState(Type type);
    }
}