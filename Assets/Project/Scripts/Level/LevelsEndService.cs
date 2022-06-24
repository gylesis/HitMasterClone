using System;
using Project.PlayerLogic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Level
{
    public class LevelsEndService : MonoBehaviour
    {
        [SerializeField] private PlayerWaypoint _finishPoint;
        
        private WaypointsPlayerMoveService _waypointsPlayerMoveService;
        private PlayerShootService _playerShootService;
        private UIContainer _uiContainer;

        [Inject]
        private void Init(WaypointsPlayerMoveService waypointsPlayerMoveService, PlayerShootService playerShootService, UIContainer uiContainer)
        {
            _uiContainer = uiContainer;
            _playerShootService = playerShootService;
            _waypointsPlayerMoveService = waypointsPlayerMoveService;
        }
        
        public void EndGame()
        {
            _uiContainer.Show("You finished");
            
            _playerShootService.AllowToShoot = false;
            
            _waypointsPlayerMoveService.Move(_finishPoint, OnPlayerArrived);
        }

        private void OnPlayerArrived()
        {
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe((l => SceneManager.LoadScene(0)));
        }
    }
}