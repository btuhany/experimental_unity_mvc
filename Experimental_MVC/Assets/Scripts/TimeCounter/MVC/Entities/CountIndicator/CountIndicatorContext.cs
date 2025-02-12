using Batuhan.MVC.Core;
using System;

namespace TimeCounter.Entities.CountIndicator
{
    internal interface ICountIndicatorContext : IContext
    {
        public Action ChangeColorEvent { get; set; }
    }
    internal class CountIndicatorContext : ICountIndicatorContext
    {
        public Action ChangeColorEvent { get; set; }
    }
}
