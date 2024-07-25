using System;
using Infrastructure.Factory;
using UnityEngine;

namespace Services.Mobs
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyTypeID EnemyTypeID;
        private IGameFactory _factory;
        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        private void Spawn()
        {
           // GameObject enemy = _factory.SpawnEnemies(EnemyTypeID, transform);
        }
    }
}
