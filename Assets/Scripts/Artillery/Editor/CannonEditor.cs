using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Features.Tools;

namespace Features.Artillery.Editor
{
    public class CannonEditor : UnityEditor.Editor
    {
        private const int SubdivisionCount = 50;
        
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Active)]
        public static void DrawGizmos(Cannon cannon, GizmoType gizmoType)
        {
            var points = new List<Vector3>();
            for (int i = 0; i <= SubdivisionCount; i++)
            {
                points.Add(BezierCurve.GetPointOnCurve(
                    cannon.StartPosition, 
                    cannon.EndPosition, 
                    cannon.MaxAltitude, 
                    (float)i/SubdivisionCount));
            }

            Gizmos.color = Color.green;
            for (int i = 0; i < points.Count - 1; i++)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }
        }
    }
}