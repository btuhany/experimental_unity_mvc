namespace Batuhan.EventBus
{
    //TODOBY Can be used flags for multiple categories
    //public enum EventCategoryID
    //{
    //    Global = 0,
    //    Service = 1,
    //    Input = 2,
    //    View = 3,
    //    Controller = 4,
    //    Model = 5
    //}
    public interface IEventCategory
    {
        public int ID { get; }
    }
}