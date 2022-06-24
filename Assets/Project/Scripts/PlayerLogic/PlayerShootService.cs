using System;
using System.Threading.Tasks;
using Project.EnemyLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.PlayerLogic
{
    public class PlayerShootService : IInitializable
    {
        private readonly PlayerFacade _playerFacade;
        private readonly BulletsPool _bulletsPool;
        private readonly Camera _camera;
        private readonly Config _config;
        private readonly InputService _inputService;

        public bool AllowToShoot { get; set; }

        private bool _shootCooldown = true;

        public PlayerShootService(PlayerFacade playerFacade, BulletsPool bulletsPool, Camera camera,
            InputService inputService, Config config)
        {
            _inputService = inputService;
            _config = config;
            _camera = camera;
            _bulletsPool = bulletsPool;
            _playerFacade = playerFacade;
        }

        public void Initialize()
        {
            _inputService.Touched.Subscribe((OnScreenTouch));

            AllowToShoot = true;
        }

        private void OnScreenTouch(Vector2 pos)
        {
            if(AllowToShoot == false) return;
            if(_shootCooldown == false) return;

            _shootCooldown = false;
            Observable.Timer(TimeSpan.FromSeconds(_config.ShootCooldown)).Subscribe((l => _shootCooldown = true));
            
            var shootDataContext = (new ShootDataContext());
            shootDataContext.TouchPos = pos;

            ScreenTouch(shootDataContext);
        }

        public void ScreenTouch(ShootDataContext context)
        {
            Transform shootTransform = _playerFacade.PlayerWeaponService.ShootTransform;

            Vector3 shootPos = shootTransform.position;
            Vector3 worldShootPoint = _camera.ScreenToWorldPoint(context.TouchPos);

            Ray screenRay = _camera.ScreenPointToRay(context.TouchPos);

            var screenRayHit = Physics.Raycast(screenRay, out var hit, 10);

            if (screenRayHit)
            {
                Vector3 shootDirection = (hit.point - shootPos).normalized;

                if (Physics.SphereCast(shootPos, 0.5f,shootDirection ,out hit, 10))
                {
                    Debug.DrawRay(shootPos, screenRay.direction, Color.black, 1);

                    var bulletSetupContext = new BulletSetupContext();

                    bulletSetupContext.Direction = shootDirection;
                    bulletSetupContext.Speed = _config.BulletSpeedModifier;

                    Shoot(bulletSetupContext, shootPos);
                }
            }
        }

        private async void Shoot(BulletSetupContext bulletSetupContext, Vector3 shootPos)
        {
            _playerFacade.Shoot(bulletSetupContext.Direction);

            await Task.Delay(100);
            
            Bullet bullet = _bulletsPool.Rent(shootPos);

            bullet.Activate();
            bullet.Setup(bulletSetupContext);

            bullet.Collision.Take(1).Subscribe((OnBulletCollision));
        }

        private void OnBulletCollision(BulletCollisionContext context)
        {
            if (context.Collider.TryGetComponent<EnemyHitbox>(out var enemyHitbox))
            {
                EnemyHitboxType hitboxType = enemyHitbox.HitboxType;

                Enemy hitboxOwner = enemyHitbox.Owner;

                var damage = _config.EnemyHitboxDamage.GetMultipliedDamage(30, hitboxType);

                Debug.Log($"{hitboxType} {damage}");

                hitboxOwner.ApplyDamage(damage);

                _bulletsPool.ReturnToPool(context.Bullet);
            }
            else if (context.Collider.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                _bulletsPool.ReturnToPool(context.Bullet);
            }
        }
    }
}