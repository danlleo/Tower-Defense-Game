using Infrastructure.Mobs;
using Infrastructure.StaticData;

namespace Services
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemy(EnemyTypeID typeID);
    }
}
