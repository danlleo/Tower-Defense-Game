using Infrastructure.AssetManagment;
using Services.Mobs;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateHUD() =>
            _assets.Instantiate(AssetPath.HUDPath);
        
        //public GameObject SpawnEnemies(EnemyTypeID enemyTypeID, Transform transform)
        //{
            
        //}
    }
}
