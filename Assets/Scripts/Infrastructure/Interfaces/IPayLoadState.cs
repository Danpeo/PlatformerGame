namespace Infrastructure
{
    public interface IPayLoadState<in TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payLoad);
    }
}