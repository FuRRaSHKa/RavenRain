using HalloGames.Architecture.StateMachine;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HalloGames.RavensRain.Gameplay.Player.States
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;

        private PriorityStateMachine _priorityStateMachine;

        private void Awake()
        {
            InitStates();
        }

        private void InitStates()
        {
            _priorityStateMachine = new PriorityStateMachine();

            IdleState idleState = new IdleState(_playerEntity);
            RunState runState = new RunState(_playerEntity);
            FireState fireState = new FireState(_playerEntity);
            DashState dashState = new DashState(_playerEntity);

            Dictionary<Type, IPriorityState> stateDictionary = new Dictionary<Type, IPriorityState>
            {
                {typeof(IdleState), idleState},
                {typeof(RunState), runState},
                {typeof(FireState), fireState},
                {typeof(DashState), dashState}
            };

            _priorityStateMachine.InitStates(stateDictionary, idleState);
        }

        public void AddState(Type state)
        {
            _priorityStateMachine.AddState(state);
        }

        public void RemoveState(Type state)
        {
            _priorityStateMachine.RemoveState(state);
        }
    }
}

