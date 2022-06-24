using UnityEngine;

namespace Project.PlayerLogic
{
    public class PlayerWeaponService : MonoBehaviour
    {
        [SerializeField] private Transform _shootTransform;

        public Transform ShootTransform => _shootTransform;
    }
}