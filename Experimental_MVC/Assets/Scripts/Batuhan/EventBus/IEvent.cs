namespace Batuhan.EventBus
{
    //TODOBY flags
    public enum EventCategoryID
    {
        Global = 0,
        Service = 1, 
        Input = 2,
        View = 3,
        Controller = 4,
        Model = 5
    }
    public interface IEventCategory
    {
        public EventCategoryID ID { get; }
    }
    public interface IEvent 
    {
        public EventCategoryID CategoryID { get; }
    }
}
