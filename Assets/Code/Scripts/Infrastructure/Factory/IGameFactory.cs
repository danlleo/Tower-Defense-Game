using Services;
using Services.Mobs;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHUD();
        GameObject CreateEnemy(EnemyTypeID enemyTypeID, Transform transform);
    }
}
