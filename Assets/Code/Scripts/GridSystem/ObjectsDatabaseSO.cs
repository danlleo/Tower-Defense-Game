using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    [CreateAssetMenu]
    public class ObjectsDatabaseSO : ScriptableObject
    {
        [field: SerializeField] public List<ObjectData> ObjectData { get; private set; }
    }
}
