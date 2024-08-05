using Mobs;
using StaticData;

namespace Services
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemy(EnemyTypeID typeID);
    }
}
