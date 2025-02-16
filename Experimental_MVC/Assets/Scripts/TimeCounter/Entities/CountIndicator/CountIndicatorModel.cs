using Batuhan.MVC.Base;
using Batuhan.MVC.Core;

namespace TimeCounter.Entities.CountIndicator
{
    internal class CountIndicatorModel : BaseModel<ICountIndicatorContext>
    {
        public int CountValue { get; set; }
        public override void Setup(ICountIndicatorContext context)
        {
            base.Setup(context);
            CountValue = 0;
        }
        public override void Dispose()
        {
        }
    }
}
