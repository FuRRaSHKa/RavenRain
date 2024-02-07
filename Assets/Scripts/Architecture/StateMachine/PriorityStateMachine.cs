using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloGames.Architecture.StateMachine
{
    public class PriorityStateMachine : StateMachine<IPriorityState>
    {
        private List<IPriorityState> _sortedStates = new List<IPriorityState>();

        public void InitStates(Dictionary<Type, IPriorityState> states, IPriorityState firstState)
        {
            InitStates(states);

            ChangeState(firstState);
            _sortedStates.Add(firstState);        
        }

        public void AddState(Type type)
        {
            IPriorityState priorityState = States[type];
            if (priorityState == null || _sortedStates.Contains(priorityState))
                return;

            _sortedStates.Add(priorityState);
            _sortedStates = _sortedStates.OrderBy(o => o.Priority).ToList();

            if (_sortedStates[0] != CurrentState)
                ChangeState(_sortedStates[0]);
        }

        public void RemoveState(Type type)
        {
            IPriorityState priorityState = States[type];
            if (priorityState == null || !_sortedStates.Contains(priorityState))
                return;

            _sortedStates.Remove(priorityState);
            if (_sortedStates.Count > 0 && _sortedStates[0] != CurrentState)
                ChangeState(_sortedStates[0]);
        }
    }

}