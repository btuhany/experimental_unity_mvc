using Batuhan.MVC.Base;
using TimeCounter.Data;
using TimeCounter.Events.ModelEvents;

namespace TimeCounter.Entities.CountIndicator
{
    internal class CountIndicatorModel : BaseModel<ICountIndicatorContext>
    {
        private CountIndicatorCommonData _data;
        public override void Setup(ICountIndicatorContext context)
        {
            base.Setup(context);
            _data = new CountIndicatorCommonData()
            {
                Color = UnityEngine.Color.white,
                Indice = 0,
                Position = UnityEngine.Vector3.zero,
            };
        }
        public void SetInitialData(CountIndicatorCommonData data)
        {
            _data = data;
            _context.EventBusModel.Publish(new CountIndicatorDataUpdatedEvent(data));
        }
        public override void Dispose()
        {
        }
    }
}
