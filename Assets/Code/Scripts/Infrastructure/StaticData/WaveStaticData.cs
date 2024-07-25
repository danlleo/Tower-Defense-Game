using System;
using Services.Mobs;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu (fileName = "WaveData", menuName = "StaticData/WaveData")]
    public class WaveStaticData : ScriptableObject
    {
        public WaveData[] WaveConfig;
    }

    [Serializable]
    public class WaveData
    {
        [field:Range(0,30)]
        [field:SerializeField] public int Delay { get; private set; }
        [field:SerializeField] public EnemyTypeID EnemiesType { get; private set; }
        [field:Range(0,10)]
        [field:SerializeField] public int EnemiesCount { get; private set; }
    }
}
