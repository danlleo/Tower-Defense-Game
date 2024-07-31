using System.Collections;
using Infrastructure.Factory;
using Infrastructure.StaticData;
using Services;
using UnityEngine;

namespace Infrastructure.Mobs
{
    public class EnemySpawner : MonoBehaviour
    {
        public LevelConfig LevelConfig;

        private IGameFactory _gameFactory;
        private int _waveNumber;
        private int _spawnIteration;

        private float _startDelay;
        private float _delay;
        private float _mobsSpawnInterval;
        private EnemyTypeID _enemiesType;
        private int _countOfEnemies;


        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }
        
        public void Spawn() =>
            StartCoroutine(SpawnFromWaveStaticData());

        private IEnumerator SpawnFromWaveStaticData()
        {
            while (_waveNumber < LevelConfig.WaveConfigs.Count)
            {
                LoadLevelData();
                yield return StartCoroutine(MobsSpawnIteration());
                _waveNumber++;
            }
        }
        
        private IEnumerator MobsSpawnIteration()
        {
            if (_waveNumber >= LevelConfig.WaveConfigs.Count)
            {
                Debug.LogError("Wave number out of range in MobsSpawnIteration.");
                yield break;
            }

            var waveConfig = LevelConfig.WaveConfigs[_waveNumber];

            for (int i = 0; i < waveConfig.WaveDataList.Count; i++)
            {
                WaveData waveData = waveConfig.WaveDataList[i];

                for (int j = 0; j < waveData.EnemiesCount; j++)
                {
                    _gameFactory.CreateEnemy(waveData.EnemiesType, transform);
                    yield return new WaitForSeconds(waveData.MobsSpawnInterval);
                }
                
                yield return new WaitForSeconds(waveData.Delay);
            }
        }

        private void LoadLevelData()
        {
            _delay = LevelConfig.WaveConfigs[_waveNumber].WaveDataList[_spawnIteration].Delay;
            _mobsSpawnInterval = LevelConfig.WaveConfigs[_waveNumber].WaveDataList[_spawnIteration].MobsSpawnInterval;
            _enemiesType = LevelConfig.WaveConfigs[_waveNumber].WaveDataList[_spawnIteration].EnemiesType;
            _countOfEnemies = LevelConfig.WaveConfigs[_waveNumber].WaveDataList[_spawnIteration].EnemiesCount;
        }
        
    }
}
