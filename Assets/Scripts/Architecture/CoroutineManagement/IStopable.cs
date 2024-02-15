namespace HalloGames.Architecture.CoroutineManagement
{
    public interface IStopable
    {
        public bool IsRunning
        {
            get;
        }

        public void Stop();
    }

}
