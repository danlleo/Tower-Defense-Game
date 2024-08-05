using System.Collections.Generic;
using System.Linq;
using Mobs;
using StaticData;
using UnityEngine;

namespace Services
{

    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeID, EnemyStaticData> _enemies;

        public void LoadEnemies()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>("StaticData/Enemies")
                .ToDictionary(x => x.EnemyTypeID, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyTypeID typeID) => 
            _enemies.TryGetValue(typeID, out EnemyStaticData staticData)
                ? staticData 
                : null;
    }
}
