using Infrastructure.AssetManagment;
using Infrastructure.StaticData;
using Services;
using Services.Mobs;
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

            return enemy;
        }
    }
}
