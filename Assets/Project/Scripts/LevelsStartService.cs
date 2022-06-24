using System.Threading.Tasks;
using Project.Level;
using Project.PlayerLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project
{
    public class LevelsStartService : IInitializable
    {
        private readonly LevelsTransitionService _levelsTransitionService;
        private readonly PlayerShootService _playerShootService;
        private readonly CameraController _cameraController;
        private readonly InputService _inputService;
        private readonly UIContainer _uiContainer;

        public LevelsStartService(LevelsTransitionService levelsTransitionService,
            PlayerShootService playerShootService, InputService inputService, CameraController cameraController, UIContainer uiContainer)
        {
            _uiContainer = uiContainer;
            _inputService = inputService;
            _cameraController = cameraController;
            _playerShootService = playerShootService;
            _levelsTransitionService = levelsTransitionService;

        }

        public void Initialize()
        {
            _inputService.Touched.Take(1).Subscribe((OnScreenTouch));
            
            _uiContainer.Show("Tap To Play");
        }

        private async void OnScreenTouch(Vector2 pos)
        {
            _uiContainer.Hide();
            _cameraController.HasToFollow = true;
            
            await Task.Delay(500);

            _playerShootService.AllowToShoot = true;
            _levelsTransitionService.GoNextLevel();
        }

       
    }
}