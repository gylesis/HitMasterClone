using System;
using UnityEngine;

namespace Project.EnemyLogic
{
    [Serializable]
    public class EnemyHitboxDamage
    {
        [Range(0f, 1f)] [SerializeField] private float _headDamage;
        [Range(0f, 1f)] [SerializeField] private float _chestDamage;
        [Range(0f, 1f)] [SerializeField] private float _armDamage;
        [Range(0f, 1f)] [SerializeField] private float _legDamage;
        public int GetMultipliedDamage(int damage, EnemyHitboxType hitboxType)
        {
            float multiplier = 1;
            
            switch (hitboxType)
            {
                case EnemyHitboxType.Head:
                    multiplier = _headDamage;
                    break;
                case EnemyHitboxType.Chest:
                    multiplier = _chestDamage;
                    break;
                case EnemyHitboxType.Leg:
                    multiplier = _legDamage;
                    break;
                case EnemyHitboxType.Arm:
                    multiplier = _armDamage;
                    break;
            }
            
            
            float dmhg = damage * (multiplier + 1);

            damage = (int)dmhg;
            
            return damage;
        }
    }
}