using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

namespace Tower
{
    public abstract class Tower : MonoBehaviour
    {
        [field: SerializeField] public float MaxShootDistance { get; private set; }
        [field: SerializeField] public float ReloadTimeInSeconds { get; private set; }
        [field: SerializeField] public float ProjectileSpeed { get; private set; }
        [field: SerializeField] public LayerMask EnemyLayerMask { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public GameObject ShootVisualEffectPrefab { get; private set; }
        protected bool IsReloading;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 1f, 0.5f, 0.2f);
            Gizmos.DrawWireSphere(transform.position, MaxShootDistance);
        }

        protected IEnumerable<IHealth> GetNearbyEnemies()
        {
            Collider[] enemiesColliders = Physics.OverlapSphere(transform.position, MaxShootDistance, EnemyLayerMask);
            
            foreach (Collider enemyCollider in enemiesColliders)
                if (enemyCollider.TryGetComponent(out IHealth enemy))
                    yield return enemy;
        }

        protected IEnumerator ReloadRoutine()
        {
            IsReloading = true;
            
            yield return new WaitForSeconds(ReloadTimeInSeconds);

            IsReloading = false;
        }

        protected void ShootVisual()
        {
            Instantiate(ShootVisualEffectPrefab);
        }
    }
}