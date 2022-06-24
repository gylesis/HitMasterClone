using UnityEngine;
using Zenject;

namespace Project.PlayerLogic
{
    public class PlayerSpawnService : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private PlayerFacade _playerFacade;

        [Inject]
        public void Init(PlayerFacade playerFacade)
        {
            _playerFacade = playerFacade;
        }

        private void Start()
        {
            var playerSpawnContext = new PlayerSpawnContext();
            playerSpawnContext.SpawnPoint = _spawnPoint;
            
            _playerFacade.SpawnPlayer(playerSpawnContext);
        }
    }
}