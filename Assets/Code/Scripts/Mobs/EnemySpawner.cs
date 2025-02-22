﻿using System.Collections;
using Infrastructure.Factory;
using Services;
using StaticData;
using UnityEngine;

namespace Mobs
{
    public class EnemySpawner : MonoBehaviour
    {
        public LevelWaveConfig _levelWaveConfig;

        private IGameFactory _gameFactory;
        private int _waveNumber;

        private void Awake() =>
            _gameFactory = AllServices.Container.Single<IGameFactory>();

        public void Spawn() =>
            StartCoroutine(SpawnFromWaveStaticData());

        private IEnumerator SpawnFromWaveStaticData()
        {
            while (_waveNumber < _levelWaveConfig.WaveConfigs.Count)
            {
                yield return StartCoroutine(MobsSpawnIteration());
                _waveNumber++;
            }
        }
        
        private IEnumerator MobsSpawnIteration()
        {
            if (_waveNumber >= _levelWaveConfig.WaveConfigs.Count)
                yield break;

            WaveConfig waveConfig = _levelWaveConfig.WaveConfigs[_waveNumber];

            foreach (WaveData waveData in waveConfig.WaveDataList)
            {
                for (int j = 0; j < waveData.EnemiesCount; j++)
                {
                    _gameFactory.CreateEnemy(waveData.EnemiesType, transform);
                    yield return new WaitForSeconds(waveData.MobsSpawnInterval);
                }
                
                yield return new WaitForSeconds(waveData.Delay);
            }
        }
    }
}
