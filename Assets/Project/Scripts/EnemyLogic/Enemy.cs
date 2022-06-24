using Project.PlayerLogic;
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
        public EnemyHitbox LastHitbox => _lastHitbox;
        public Vector3 LastHitPoint => _lastHitPoint;
        public EnemyRagdoll Ragdoll => _ragdoll;
        
        private HealthData _healthData;
        private EnemyHitbox _lastHitbox;
        private Vector3 _lastHitPoint;

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
        

        public void ApplyDamage(EnemyHitContext context)
        {
            _lastHitbox = context.Hitbox;
            _lastHitPoint = context.HitPoint;
            
            _healthData.ApplyDamage(context.Damage);
        }
    }
}