using UnityEngine;

namespace Project.EnemyLogic
{
    [RequireComponent(typeof(Collider))]
    public class EnemyHitbox : MonoBehaviour
    {
        [SerializeField] private EnemyHitboxType _hitboxType;
        private Enemy _enemy;

        public Enemy Owner => _enemy;
        
        public EnemyHitboxType HitboxType => _hitboxType;

        public void Init(Enemy enemy)
        {
            _enemy = enemy;
        }
       
    }
    
    public enum EnemyHitboxType
    {
        Head,
        Chest,
        Leg,
        Arm
    }
    
}