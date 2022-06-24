using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.PlayerLogic
{
    public class BulletsPool : IInitializable
    {
        private readonly Dictionary<Bullet, bool> _bullets = new Dictionary<Bullet, bool>();

        private readonly BulletsFactory _bulletsFactory;
        private readonly Config _config;

        public BulletsPool(BulletsFactory bulletsFactory, Config config)
        {
            _config = config;
            _bulletsFactory = bulletsFactory;
        }

        public void Initialize()
        {
            var bulletsAmount = _config.InitBulletsAmount;
            
            CreateBalls(bulletsAmount);
        }

        public void CreateBalls(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                Bullet bullet = _bulletsFactory.Create();

                _bullets.Add(bullet, true);
                OnEnterPool(bullet);
            }
        }

        private void OnEnterPool(Bullet bullet)
        {
            bullet.Disable();
            
            _bullets[bullet] = true;
        }

        public Bullet Rent(Vector3 pos)
        {
            var availableBullet = _bullets.FirstOrDefault(x => x.Value == true).Key;

            if (availableBullet == null)
            {
                CreateBalls(_bullets.Count * 2);
                return Rent(pos);
            }
            
            availableBullet.transform.position = pos;
            
            _bullets[availableBullet] = false;

            return availableBullet;
        }

        public void ReturnToPool(Bullet bullet)
        {
            OnEnterPool(bullet);
        }
    }

    public struct BulletSetupContext
    {
        public Vector3 Direction;
        public float Speed;
    }
}