using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EnemyLogic
{
    public class EnemyHealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        
        private HealthData _healthData;
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        public void Init(HealthData healthData)
        {
            _healthData = healthData;
            
            healthData.Changed.Subscribe((OnHealthChanged)).AddTo(_compositeDisposable);
            healthData.ZeroHealth.Subscribe((OnZeroHealth)).AddTo(_compositeDisposable);
        }

        private void OnZeroHealth(Unit _)
        {
            _healthBar.enabled = false;
        }

        private void OnHealthChanged(int health)
        {
            var healthDataMaxValue = (float) health / _healthData.MaxValue;

            healthDataMaxValue = Mathf.Clamp(healthDataMaxValue, 0.1f, 1);
            
            _healthBar.fillAmount = healthDataMaxValue;
        }


        private void OnDestroy()
        {
            _compositeDisposable.Dispose();
        }
    }
}