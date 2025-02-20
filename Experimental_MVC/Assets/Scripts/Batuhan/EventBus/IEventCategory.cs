namespace Batuhan.EventBus
{
    public interface IEventCategory
    {
        public int ID { get; }
        public bool CanBeDisposed { get; }
    }
}