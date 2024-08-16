using Logic;
using UnityEngine;

namespace Tower
{
    [DisallowMultipleComponent]
    public abstract class TowerNoTarget : Tower
    {
        private void Update()
        {
            Shoot();
        }

        protected abstract void OnShoot();

        private void Shoot()
        {
            if (IsReloading) return;

            // ShootVisual();
            
            foreach (IHealth health in GetNearbyEnemies()) 
                health.TakeDamage(Damage);
            
            OnShoot();
            
            StartCoroutine(ReloadRoutine());
        }
    }
}