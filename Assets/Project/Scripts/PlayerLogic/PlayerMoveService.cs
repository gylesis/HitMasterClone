using Project.Level;
using UnityEngine;
using Zenject;

namespace Project.PlayerLogic
{
    public class PlayerMoveService : ITickable
    {
        private LevelsTransitionService _levelsTransitionService;
        private InputService _inputService;

        private PlayerMoveService(LevelsTransitionService levelsTransitionService, InputService inputService)
        {
            _inputService = inputService;
            _levelsTransitionService = levelsTransitionService;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _levelsTransitionService.GoNextLevel();
            }

        }
    }
}