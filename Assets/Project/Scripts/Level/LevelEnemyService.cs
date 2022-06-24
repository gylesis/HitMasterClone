using System.Collections.Generic;
using Project.EnemyLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Level
{
    public class LevelEnemyService : MonoBehaviour
    {
        public Subject<EnemyDeathContext> EnemyDied { get; } = new Subject<EnemyDeathContext>();

        private readonly List<Enemy> _enemies = new List<Enemy>();
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        private Level _level;
        public int AliveEnemies => _enemies.Count;
        
        [Inject]
        private void Init(Level level, LevelEnemySpawner levelEnemySpawner)
        {
            _level = level;

            levelEnemySpawner.Spawned.Subscribe((OnEnemySpawned)).AddTo(_compositeDisposable);
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            enemy.ZeroHealth.Take(1).Subscribe(OnZeroHealth).AddTo(_compositeDisposable);
            _enemies.Add(enemy);
        }

        private void OnZeroHealth(Enemy enemy)
        {
            var enemyDeathContext = new EnemyDeathContext();
            enemyDeathContext.Enemy = enemy;
            enemyDeathContext.Level = _level;

            _enemies.Remove(enemy);
            
            enemy.Ragdoll.Activate();

            var vector3 = new Vector3(Random.value,-Random.value,Random.value);

            Vector3 enemyLastHitPoint = enemy.LastHitPoint;
            Vector3 lastHitboxPos = enemy.LastHitbox.transform.position;

            enemy.LastHitbox.GetComponent<Rigidbody>().velocity += (enemyLastHitPoint - lastHitboxPos).normalized * 20;
            
           // enemy.Ragdoll.Force(vector3 * 5);
            
            EnemyDied.OnNext(enemyDeathContext);
        }
        
        private void OnDestroy()
        {
            _compositeDisposable.Dispose();
        }
    }

    public struct EnemyDeathContext
    {
        public Level Level;
        public Enemy Enemy;
    }
    
}