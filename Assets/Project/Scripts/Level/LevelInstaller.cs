using Project.EnemyLogic;
using UnityEngine;
using Zenject;

namespace Project.Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelData _levelData;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _enemiesParent;
        public override void InstallBindings()
        {
            Container.Bind<LevelData>().FromInstance(_levelData).AsSingle();
            Container.Bind<Level>().FromComponentOnRoot().AsSingle();
            
            var enemySpawnPoints = GetComponentsInChildren<EnemySpawnPoint>();

            Container.Bind<EnemySpawnPoint[]>().FromInstance(enemySpawnPoints).AsSingle();

            Container.BindInterfacesAndSelfTo<LevelEnemySpawner>().AsSingle().NonLazy();
            
            Container
                .BindFactory<EnemySpawnContext, Enemy, EnemyFactory>()
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransform(_enemiesParent)
                .AsSingle();
        }
    }
}