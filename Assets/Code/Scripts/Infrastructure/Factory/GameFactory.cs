using Infrastructure.AssetManagment;
using Logic.PathFinders;
using Mobs;
using Services;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assets;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetsProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateHUD() =>
            _assets.Instantiate(AssetPath.HUDPath);
        
        public GameObject CreateEnemy(EnemyTypeID typeID, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(typeID);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);

          //  enemy.GetComponent<EnemyMove>;
       
            return enemy;
        }
    }
}
