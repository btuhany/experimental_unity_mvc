using Batuhan.MVC.UnityComponents.Base;
using System;

namespace SnakeExample.Entities.Snake
{
    public class SnakeView : BaseViewMonoBehaviour
    {
        public override Type ContractTypeToBind => typeof(SnakeView);
    }
}