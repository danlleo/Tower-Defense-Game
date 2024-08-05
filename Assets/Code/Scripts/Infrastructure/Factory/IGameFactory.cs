using Mobs;
using Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHUD();
        GameObject CreateEnemy(EnemyTypeID enemyTypeID, Transform transform);
    }
}
