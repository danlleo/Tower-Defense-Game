using Infrastructure.Factory;
using UnityEngine;

namespace Services.Mobs
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyTypeID EnemyTypeID;
        private IGameFactory _gameFactory;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void Spawn()
        {
            _gameFactory.CreateEnemy(EnemyTypeID, transform);
        }
    }
}
