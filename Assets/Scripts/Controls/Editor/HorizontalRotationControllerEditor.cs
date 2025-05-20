using UnityEditor;
using UnityEngine;

namespace Features.Controls.Editor
{
    public class HorizontalRotationControllerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Active)]
        public static void DrawGizmos(HorizontalRotationController controller, GizmoType gizmoType)
        {
            Vector3 minAngle = controller.transform.rotation.eulerAngles;
            Vector3 maxAngle = controller.transform.rotation.eulerAngles;
            minAngle.y -= controller.EditorMaxAngle;
            maxAngle.y += controller.EditorMaxAngle;
            
            Vector3 minAngleDirection = Quaternion.Euler(minAngle) * Vector3.forward;
            Vector3 maxAngleDirection = Quaternion.Euler(maxAngle) * Vector3.forward;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(controller.EditorTargetTransform.position, minAngleDirection * 100f);
            Gizmos.DrawRay(controller.EditorTargetTransform.position, maxAngleDirection * 100f);
        }
    }
}