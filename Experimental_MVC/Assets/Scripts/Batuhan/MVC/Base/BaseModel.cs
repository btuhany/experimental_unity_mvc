using Batuhan.MVC.Core;
using System;

namespace Batuhan.MVC.Base
{
    public abstract class BaseModel : IModel, IContextHolder
    {
        public abstract IContext Context { get; }

    }
}
