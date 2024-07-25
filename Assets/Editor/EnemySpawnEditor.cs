using Services.Mobs;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}
