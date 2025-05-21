using UnityEngine;
using UnityEditor;

namespace Features.Units.Editor
{
    public class UnitSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Active)]
        public static void DrawGizmos(UnitSpawner unitSpawner, GizmoType gizmoType)
        {
            Vector3 rightDirection = unitSpawner.transform.right * unitSpawner.EditorSpawnRange;
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(unitSpawner.transform.localPosition - rightDirection, 1f);
            Gizmos.DrawSphere(unitSpawner.transform.localPosition + rightDirection, 1f);
            Gizmos.DrawLine(unitSpawner.transform.localPosition - rightDirection, 
                unitSpawner.transform.localPosition + rightDirection);
        }
    }
}