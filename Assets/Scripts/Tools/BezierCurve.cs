using UnityEngine;

namespace Features.Tools
{
    public static class BezierCurve
    {
        public static Vector3 GetPointOnCurve(Vector3 start, Vector3 end, float altitude, float t)
        {
            Vector3 mid = Vector3.Lerp(start, end, t);
            float arc = 4f * altitude * t * (1f - t);
            
            return new Vector3(mid.x, mid.y + arc, mid.z);
        }
    }
}