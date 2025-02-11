namespace Batuhan.EventBus
{
    public interface IEvent 
    {
        public EventCategoryID CategoryID { get; }
    }
}
