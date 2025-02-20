using Batuhan.MVC.Core;
using R3;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextModel :  IModelContextual<ICounterTextContext>
    {
        public void UpdateTextWithTickValue(int value);
        void UpdateAnimatorSpeed(float value);
        ReactiveProperty<string> CounterText { get; }
        public ReadOnlyReactiveProperty<float> AnimatorSpeed { get; }
        public int TriggerHash { get; }
    }
    public class CounterTextModel : ICounterTextModel
    {
        private const float INITIAL_ANIMATOR_SPEED = 1.0f;
        private const float MIN_ANIMATOR_SPEED = 0.5f;
        private const float MAX_ANIMATOR_SPEED = 10.0f;
        private ICounterTextContext _context;
        public ICounterTextContext Context => _context;

        public ReactiveProperty<string> CounterText { get; private set; }
        private ReactiveProperty<float> _animatorSpeed;
        public ReadOnlyReactiveProperty<float> AnimatorSpeed => _animatorSpeed;

        private readonly int _triggerHash = UnityEngine.Animator.StringToHash("trigger");

        public int TriggerHash => _triggerHash;


        public void Setup(ICounterTextContext context)
        {
            _context = context;
            _context.Debug.Log("Setup", this);
            CounterText = new(string.Empty);
            _animatorSpeed = new (INITIAL_ANIMATOR_SPEED);
        }

        public void Dispose()
        {
            CounterText?.Dispose();
        }

        public void UpdateTextWithTickValue(int value)
        {
            CounterText.Value = value.ToString();
        }
        public void UpdateAnimatorSpeed(float value)
        {
            _animatorSpeed.Value = UnityEngine.Mathf.Clamp(value, MIN_ANIMATOR_SPEED, MAX_ANIMATOR_SPEED);
        }
    }
}
