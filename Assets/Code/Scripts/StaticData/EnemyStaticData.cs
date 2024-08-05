using Mobs;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy", order = 0)]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeID EnemyTypeID;
    
        [Range(1,100)] public int Hp = 50;
        [Range(1,30)] public float Damage = 10;
        [Range(.5f,1)] public float AttackRange = .5f;
        [Range(0,10)] public float MoveSpeed = 3;
    
        public GameObject Prefab;
    }
}
