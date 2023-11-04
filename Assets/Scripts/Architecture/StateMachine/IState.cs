namespace HalloGames.Architecture.StateMachine
{
    public interface IState
    {
        public void Enter();

        public void Exit();
    }

    public interface IPriorityState : IState
    {
        public int Priority
        {
            get;
        }
    }
}