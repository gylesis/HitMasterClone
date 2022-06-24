using Project.Level;
using Project.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Level.Level[] _levels;
        [SerializeField] private Player _player;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletsParent;
        [SerializeField] private Camera _camera;
        [SerializeField] private Config _config;
        [SerializeField] private LevelsEndService _levelsEndService;
        [SerializeField] private UIContainer _uiContainer;
        [SerializeField] private CameraController _cameraController;  
        
        public override void InstallBindings()
        {
            Container
                .Bind<Config>()
                .FromInstance(_config)
                .AsSingle();

            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle();
            
            Container.Bind<LevelsEndService>().FromInstance(_levelsEndService).AsSingle();

            Container.Bind<UIContainer>().FromInstance(_uiContainer).AsSingle().NonLazy();

            Container
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<Camera>()
                .FromInstance(_camera)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerShootService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<GameObject>()
                .FromInstance(_player.gameObject)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<BulletsPool>()
                .AsSingle();

            Container
                .BindFactory<Bullet, BulletsFactory>()
                .FromComponentInNewPrefab(_bulletPrefab)
                .UnderTransform(_bulletsParent)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerMoveService>()
                .AsSingle();
            Container
                .Bind<PlayerFacade>()
                .FromSubContainerResolve()
                .ByInstaller<PlayerInstaller>()
                .AsSingle();

            Container
                .Bind<LevelsTransitionService>()
                .AsSingle();

            Container
                .Bind<WaypointsPlayerMoveService>()
                .AsSingle();
            Container
                .Bind<LevelsController>()
                .AsSingle()
                .WithArguments(_levels);

            Container.BindInterfacesAndSelfTo<LevelsStartService>().AsSingle();

        }
    }
}