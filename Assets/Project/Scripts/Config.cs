using Project.EnemyLogic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "Config", fileName = "Config", order = 0)]
    public class Config : ScriptableObject
    {
        [SerializeField] private int _initBulletsAmount;
        [SerializeField] private float _bulletSpeedModifier = 20;
        [SerializeField] private int _botHealth = 100;
        [SerializeField] private EnemyHitboxDamage _enemiesHitboxDamage;
        [SerializeField] private float _shootCooldown = 0.2f;

        public float ShootCooldown => _shootCooldown;
        public int BotHealth => _botHealth;
        public EnemyHitboxDamage EnemyHitboxDamage => _enemiesHitboxDamage;
        public int InitBulletsAmount => _initBulletsAmount;
        public float BulletSpeedModifier => _bulletSpeedModifier;
    }
}