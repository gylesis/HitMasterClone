using System;
using UniRx;
using UnityEngine;

namespace Project.PlayerLogic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Collider _collider;

        private float _speed;
        private Vector3 _direction;
        private IDisposable _updatePosDisposable;

        public Subject<BulletCollisionContext> Collision { get; } = new Subject<BulletCollisionContext>();
        
        public void Setup(BulletSetupContext context)
        {
            Stop();

            _direction = context.Direction;
            _speed = context.Speed;

            transform.forward = _direction;
            
            _updatePosDisposable = Observable.EveryUpdate().Subscribe(_ => UpdatePos());
        }

        private void OnTriggerEnter(Collider other)
        {
            var bulletCollisionContext = new BulletCollisionContext();
            
            bulletCollisionContext.Bullet = this;
            bulletCollisionContext.Collider = other;
            
            Collision.OnNext(bulletCollisionContext);
        }

        private void Stop()
        {
            _updatePosDisposable?.Dispose();
            
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void Activate()
        {
            _meshRenderer.enabled = true;
            _collider.enabled = true;
        }

        public void Disable()
        {
            Stop();
            
            _meshRenderer.enabled = false;
            _collider.enabled = false;
        }
        
        private void UpdatePos()
        {
            _rigidbody.velocity = _direction * _speed * Time.deltaTime;
        }
    }

    public struct BulletCollisionContext
    {
        public Bullet Bullet;
        public Collider Collider;
    }
    
}