using System;
using System.Linq;
using Logic;
using UnityEngine;

namespace Tower
{
    public abstract class TowerTarget : Tower
    {
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
        
        protected IHealth Target { get; private set; }
        
        private void Update()
        {
            if (IsReloading) return;
            
            Target = GetNearbyEnemies().FirstOrDefault();
            
            ShootAt(Target);
        }

        protected abstract void OnShoot();
        
        public abstract void LaunchProjectileAt(IHealth enemy, Action onHitAction);
        
        private void ShootAt(IHealth target)
        {
            if (target == null) return;

            // ShootVisual();
            
            // Projectile projectile = Instantiate(ProjectilePrefab);
            // projectile.Launch(transform.position, target.position, ProjectileSpeed);
            
            LaunchProjectileAt(target, () => target.TakeDamage(Damage));
            
            OnShoot();
            
            StartCoroutine(ReloadRoutine());
        }
    }
}