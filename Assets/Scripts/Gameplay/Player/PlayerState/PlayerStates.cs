using HalloGames.Architecture.StateMachine;

using Debug = UnityEngine.Debug;

namespace HalloGames.RavensRain.Gameplay.Player.States
{
    public abstract class PlayerState : IPriorityState
    {
        protected readonly PlayerEntity playerEntity;

        public abstract int Priority
        {
            get;
        }

        public PlayerState(PlayerEntity playerEntity)
        {
            this.playerEntity = playerEntity;
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
            playerEntity.WeaponController.EnableWeapon();
        }

        public override void Exit()
        {
            playerEntity.WeaponController.DisableWeapon();
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
            playerEntity.PlayerMovement.SetRun(true);
        }

        public override void Exit()
        {
            playerEntity.PlayerMovement.SetRun(false);
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
            playerEntity.PlayerMovement.SetDash(true);
        }

        public override void Exit()
        {
            playerEntity.PlayerMovement.SetDash(false);
        }
    }
}