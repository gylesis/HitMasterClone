using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerWeaponService _playerWeaponService;

        public PlayerWeaponService PlayerWeaponService => _playerWeaponService;

        private IDisposable _disposable;
        
        private static readonly int Fire = Animator.StringToHash("Fire");
        private static readonly int Velocity = Animator.StringToHash("Velocity");
        private static readonly int MoveTrigger = Animator.StringToHash("Move");

        [Inject]
        private void Init(PlayerSpawnContext context) { }

        public void Move(Vector3 pos, Action onArrived = null)
        {
            _animator.SetTrigger(MoveTrigger);
            _disposable?.Dispose();
            _navMeshAgent.SetDestination(pos);

            _disposable = Observable.EveryUpdate().Subscribe((l =>
            {
                var velocity = _navMeshAgent.velocity.sqrMagnitude;

                if (velocity != 0)
                {
                    _animator.SetFloat(Velocity, velocity / 1);
                }
                else
                {
                    _animator.SetFloat(Velocity, 0);
                    onArrived?.Invoke();
                    _disposable?.Dispose();
                }
            }));
        }
        public void Shoot(Vector3 direction)
        {
            _animator.SetTrigger(Fire);

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            var yAngle = transform.rotation.eulerAngles.y;

            DOVirtual.Float(yAngle, lookRotation.eulerAngles.y, 1.5f, (value =>
            {
                Vector3 eulerAngles = transform.rotation.eulerAngles;
                eulerAngles.y = value;
                transform.rotation = Quaternion.Euler(eulerAngles);
            }));
            
        }
        
    }

    public struct ShootDataContext  
    {
        public Vector2 TouchPos;
    }
}