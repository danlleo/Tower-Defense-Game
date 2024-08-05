using System;
using System.Collections.Generic;
using Mobs;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelWaveConfig", menuName = "StaticData/LevelConfig")]
    public class LevelWaveConfig : ScriptableObject
    {
        [SerializeField] private List<WaveConfig> _waveConfigs = new List<WaveConfig>();
        public List<WaveConfig> WaveConfigs => _waveConfigs;
    }

    [Serializable]
    public class WaveConfig
    {
        [SerializeField] private string _name;
        public string Name => _name;

        [SerializeField] private List<WaveData> _waveDataList = new List<WaveData>();
        public List<WaveData> WaveDataList => _waveDataList;
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
