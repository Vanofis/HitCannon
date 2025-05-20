using UnityEditor;
using UnityEngine;

namespace Features.Controls.Editor
{
    [CustomEditor(typeof(TurretRotationController))]
    public class TurretRotationControllerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.InSelectionHierarchy)]
        public static void DrawGizmo(TurretRotationController controller, GizmoType gizmoType)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(controller.EditorTurretPosition, controller.EditorDeadZoneRadius);
        }
    }
}