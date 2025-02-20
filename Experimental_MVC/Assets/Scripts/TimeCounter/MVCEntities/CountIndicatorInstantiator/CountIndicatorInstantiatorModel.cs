using Batuhan.MVC.Base;
using UnityEngine;
using UnityEngine.UIElements;


namespace TimeCounter.Entities.CountIndicatorInstantiator
{
    internal class CountIndicatorInstantiatorModel : BaseModel<ICountIndicatorInstantiatorContext>
    {
        private const float INITIAL_RADIUS = 2.0f;
        private const float RADIUS_INCREASE_VALUE = 1.0f;
        private const float COS_INTERVAL = 20.0f;
        private const float SIN_INTERVAL = 20.0f;

        private float _cosAngle = 0;
        private float _sinAngle = 0;

        public UnityEngine.Vector3 CalcPosition(int index)
        {
            _cosAngle = (COS_INTERVAL * index);
            _sinAngle = (SIN_INTERVAL * index);

            var sumAngle = _cosAngle + _sinAngle;
            float completedCircles = Mathf.Floor(sumAngle / 720.0f);
            var radius = INITIAL_RADIUS + completedCircles * RADIUS_INCREASE_VALUE;

            _cosAngle %= 360.0f;
            _sinAngle %= 360.0f;

            var xPos = UnityEngine.Mathf.Cos(_cosAngle * Mathf.PI / 180.0f);
            var yPos = UnityEngine.Mathf.Sin(_sinAngle * Mathf.PI / 180.0f);

            var posVec2 = new UnityEngine.Vector2(xPos, yPos).normalized * radius;

            return new UnityEngine.Vector3(posVec2.x, posVec2.y, 1.0f);
        }
    }
}
