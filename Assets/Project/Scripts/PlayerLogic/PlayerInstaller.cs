using UnityEngine;
using Zenject;

namespace Project.PlayerLogic
{
    public class PlayerInstaller : Installer
    {
        private GameObject _gameObject;

        [Inject]
        private void Init(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public override void InstallBindings()
        {
            Container.Bind<PlayerFacade>().AsSingle();
            Container.BindFactory<PlayerSpawnContext, Player, PlayerFactory>().FromComponentInNewPrefab(_gameObject)
                .AsSingle();
        }
    }
}