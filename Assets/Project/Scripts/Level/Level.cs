using System;
using Project.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Project.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private PlayerWaypoint _playerWaypoint;
        [SerializeField] private LevelEnemyService _levelEnemyService;
      
        private LevelData _levelData;
        public int Id => _levelData.Id;

        public PlayerWaypoint PlayerWaypoint => _playerWaypoint;
        public LevelEnemyService LevelEnemyService => _levelEnemyService;

        [Inject]
        private void Init(LevelData levelData)
        {   
            _levelData = levelData;
        }
        
    }


    [Serializable]
    public class LevelData
    {
        public int Id;
    }
    
}