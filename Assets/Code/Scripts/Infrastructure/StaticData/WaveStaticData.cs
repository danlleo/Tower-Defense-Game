using System;
using System.Collections.Generic;
using Infrastructure.Mobs;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "StaticData/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<WaveConfig> waveConfigs = new();
        public List<WaveConfig> WaveConfigs => waveConfigs;
    }

    [Serializable]
    public class WaveConfig
    {
        [SerializeField] private string name;
        public string Name => name;

        [SerializeField] private List<WaveData> waveDataList = new();
        public List<WaveData> WaveDataList => waveDataList;
    }

    [Serializable]
    public class WaveData
    {
        [field: Range(0, 30)]
        [field: SerializeField] public float Delay { get; private set; }
        [field: Range(0, 10)]
        [field: SerializeField] public float MobsSpawnInterval;
        [field: SerializeField] public EnemyTypeID EnemiesType { get; private set; }
        [field: Range(0, 10)]
        [field: SerializeField] public int EnemiesCount { get; private set; }
    }
}
