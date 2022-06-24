using System;
using UniRx;
using UnityEngine;

namespace Project.PlayerLogic
{
    public class PlayerFacade
    {
        private Player _player;
        private PlayerFactory _playerFactory;
       
        private IDisposable _shootingDisposable;

        public PlayerWeaponService PlayerWeaponService => _player.PlayerWeaponService;
        public Transform Transform => _player.transform;
        
        public PlayerFacade(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public void SpawnPlayer(PlayerSpawnContext playerSpawnContext)
        {
            _player = _playerFactory.Create(playerSpawnContext);
        }

        public void Move(Vector3 pos, Action onArrived = null)
        {
            if (_player == null)
            {
                Debug.LogError("Player isn't spawned");
                return;
            }
            
            _player.Move(pos, onArrived);
        }

        public void Shoot(Vector3 direction)
        {
            _player.Shoot(direction);
        }

    }
}