using UnityEngine;

namespace GridSystems
{
    [System.Serializable]
    public struct ObjectData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public Vector2Int Size { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}