using Batuhan.MVC.Base;
using UnityEngine;


namespace TimeCounter.Entities.CountIndicatorInstantiator
{
    internal class CountIndicatorInstantiatorModel : BaseModel<ICountIndicatorInstantiatorContext>
    {
        private const float RADIUS_INCREASE_VALUE = 1.0f;
        private const float COS_INTERVAL = 20.0f;
        private const float SIN_INTERVAL = 20.0f;

        private float _radius = 2.0f;
        private float _cosAngle = 0;
        private float _sinAngle = 0;

        public UnityEngine.Vector3 CalcAndGetNextPosition()
        {
            _cosAngle += COS_INTERVAL;
            _sinAngle += SIN_INTERVAL;


            if (_cosAngle > 360.0f && _sinAngle > 360.0f)
            {
                _cosAngle -= 360.0f;
                _sinAngle -= 360.0f;
                _radius += RADIUS_INCREASE_VALUE;
            }

            var xPos = UnityEngine.Mathf.Cos(_cosAngle * Mathf.PI / 180.0f);
            var yPos = UnityEngine.Mathf.Sin(_sinAngle * Mathf.PI / 180.0f);

            var posVec2 = new UnityEngine.Vector2(xPos, yPos).normalized * _radius;

            return new UnityEngine.Vector3(posVec2.x, posVec2.y, 1.0f);
        }
    }
}
