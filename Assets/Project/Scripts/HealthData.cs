using UniRx;
using UnityEngine;

namespace Project
{
    public class HealthData 
    {
        private int _value;
        public int Value => _value;

        private int _maxValue;
        public int MaxValue => _maxValue;
        
        public Subject<Unit> ZeroHealth { get; } = new Subject<Unit>();
        public Subject<int> Changed { get; } = new Subject<int>();

        public HealthData(int health)
        {
            _value = health;
            _maxValue = _value;
        }
        
        public void ApplyDamage(int damage)
        {
            if (damage < 0) return;

            _value -= damage;
            _value = Mathf.Clamp(_value, 0, _maxValue);

            Changed.OnNext(_value);
            
            if (_value <= 0)
            {
                ZeroHealth.OnNext(Unit.Default);
            }
        }
        
    }
}