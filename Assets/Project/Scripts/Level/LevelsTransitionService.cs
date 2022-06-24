using System;
using Project.PlayerLogic;
using UniRx;

namespace Project.Level
{
    public class LevelsTransitionService : IDisposable
    {
        private readonly LevelsController _levelsController;
        private readonly WaypointsPlayerMoveService _waypointsPlayerMoveService;
        private readonly LevelsEndService _levelsEndService;
        private IDisposable _enemyDiedDisposable;
        private readonly PlayerShootService _playerShootService;
        private readonly CameraController _cameraController;

        public LevelsTransitionService(LevelsController levelsController, 
            WaypointsPlayerMoveService waypointsPlayerMoveService,
            LevelsEndService levelsEndService, PlayerShootService playerShootService, CameraController cameraController)
        {
            _cameraController = cameraController;
            _playerShootService = playerShootService;
            _levelsEndService = levelsEndService;
            _waypointsPlayerMoveService = waypointsPlayerMoveService;
            _levelsController = levelsController;
        }
        
        public void GoNextLevel()
        {
            var existMoreLevels = _levelsController.DoesExistMoreLevels();

            if (existMoreLevels)
            {
                Level nextLevel = _levelsController.GetNextLevel();

                Handle(nextLevel);

                _playerShootService.AllowToShoot = false;
                _cameraController.HasToFollow = true;
                
                _waypointsPlayerMoveService.Move(nextLevel.PlayerWaypoint, (() =>
                {
                    _playerShootService.AllowToShoot = true;
                    _cameraController.HasToFollow = false;
                    _cameraController.RotateTowards(nextLevel.PlayerWaypoint.transform.rotation);
                }));
            }
            else
            {
                _cameraController.HasToFollow = true;
                
                _levelsEndService.EndGame();
            }
        }

        public void Handle(Level level)
        {
            _enemyDiedDisposable?.Dispose();

            _enemyDiedDisposable = level.LevelEnemyService.EnemyDied.Subscribe((OnEnemyDied));
        }

        private void OnEnemyDied(EnemyDeathContext context)
        {
            var aliveEnemies = context.Level.LevelEnemyService.AliveEnemies;

            if (aliveEnemies == 0)
            {
                FinishLevel(context.Level);
            }
        }
        
        private void FinishLevel(Level level)
        {
            GoNextLevel();
        }

        public void Dispose()
        {
            _enemyDiedDisposable?.Dispose();
        }
    }
}