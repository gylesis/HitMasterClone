using UniRx;
using UnityEngine;
using Zenject;

namespace Project.EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyHitbox[] _hitboxes;
        [SerializeField] private EnemyHealthView _healthView;
        [SerializeField] private EnemyRagdoll _ragdoll;

        public EnemyRagdoll Ragdoll => _ragdoll;

        private HealthData _healthData;

        private void Reset()
        {
            _hitboxes = GetComponentsInChildren<EnemyHitbox>();
        }

        public Subject<Enemy> ZeroHealth { get; } = new Subject<Enemy>();

        [Inject]
        private void Init(EnemySpawnContext spawnContext)
        {
            _hitboxes = GetComponentsInChildren<EnemyHitbox>();
            
            foreach (EnemyHitbox enemyHitbox in _hitboxes) 
                enemyHitbox.Init(this);

            _healthData = new HealthData(spawnContext.Hp);

            _healthData.ZeroHealth.Take(1).Subscribe((unit => ZeroHealth.OnNext(this)));
            
            _healthView.Init(_healthData);
        }
        

        public void ApplyDamage(int damage)
        {
            _healthData.ApplyDamage(damage);
        }
    }
}