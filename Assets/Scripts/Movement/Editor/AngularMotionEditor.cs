using UnityEditor;
using UnityEngine;

namespace Features.Movement.Editor
{
    public class AngularMotionEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Active)]
        public static void DrawGizmos(AngularRotation motion, GizmoType gizmoType)
        {
            Vector3 minAngleDirection = Quaternion.Euler(new Vector3(0f, -motion.EditorMaxAngle, 0f)) * Vector3.forward;
            Vector3 maxAngleDirection = Quaternion.Euler(new Vector3(0f, motion.EditorMaxAngle, 0f)) * Vector3.forward;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(motion.EditorDirectionTransform.position, minAngleDirection * 100f);
            Gizmos.DrawRay(motion.EditorDirectionTransform.position, maxAngleDirection * 100f);
        }
    }
}