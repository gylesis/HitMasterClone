using Project.EnemyLogic;
using UniRx;
using Zenject;

namespace Project.Level
{
    public class LevelEnemySpawner : IInitializable
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemySpawnPoint[] _enemySpawnPoints;
        private readonly Config _config;

        public Subject<Enemy> Spawned { get; } = new Subject<Enemy>();

        public LevelEnemySpawner(EnemyFactory enemyFactory, EnemySpawnPoint[] enemySpawnPoints, Config config)
        {
            _config = config;
            _enemySpawnPoints = enemySpawnPoints;
            _enemyFactory = enemyFactory;
        }

        public void Initialize()
        {
            foreach (EnemySpawnPoint spawnPoint in _enemySpawnPoints)
            {
                var enemySpawnContext = new EnemySpawnContext();
                enemySpawnContext.SpawnPoint = spawnPoint;
                enemySpawnContext.Hp = _config.BotHealth;

                Enemy enemy = _enemyFactory.Create(enemySpawnContext);

                Spawned.OnNext(enemy);
            }
        }
    }
}