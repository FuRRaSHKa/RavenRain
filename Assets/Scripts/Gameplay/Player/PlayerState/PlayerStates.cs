using HalloGames.Architecture.StateMachine;

using Debug = UnityEngine.Debug;

namespace HalloGames.RavensRain.Gameplay.Player.States
{
    public abstract class PlayerState : IPriorityState
    {
        private readonly PlayerEntity _playerEntity;

        protected PlayerEntity PlayerEntity => _playerEntity;

        public abstract int Priority
        {
            get;
        }

        public PlayerState(PlayerEntity playerEntity)
        {
            this._playerEntity = playerEntity;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }

    public class IdleState : PlayerState
    {
        public IdleState(PlayerEntity playerEntity) : base(playerEntity)
        {

        }

        public override int Priority => 10;

    }

    public class FireState : PlayerState
    {
        public FireState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        public override int Priority => 1;
    
        public override void Enter()
        {
            PlayerEntity.WeaponController.EnableWeapon();
        }

        public override void Exit()
        {
            PlayerEntity.WeaponController.DisableWeapon();
        }
    }

    public class RunState : PlayerState
    {
        public RunState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        public override int Priority => 2;

        public override void Enter()
        {
            PlayerEntity.PlayerMovement.SetRun(true);
        }

        public override void Exit()
        {
            PlayerEntity.PlayerMovement.SetRun(false);
        }
    }

    public class DashState : PlayerState
    {
        public DashState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        public override int Priority => 0;

        public override void Enter()
        {
            PlayerEntity.PlayerMovement.SetDash(true);
        }

        public override void Exit()
        {
            PlayerEntity.PlayerMovement.SetDash(false);
        }
    }
}