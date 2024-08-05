using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<WaveConfig> EnemyWaves;
    public List<Vector3> PathFinderKeyPoints;
  }
}