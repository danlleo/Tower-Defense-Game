using Infrastructure.StaticData;
using Services.Mobs;

namespace Services
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemy(EnemyTypeID typeID);
    }
}
