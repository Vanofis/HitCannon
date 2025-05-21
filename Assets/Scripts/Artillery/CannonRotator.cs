using UnityEngine;
using Features.Tools;

namespace Features.Artillery
{
    public class CannonRotator : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Data
        
        
        [SerializeField] 
        private float rotationSpeed = 20f;
        [SerializeField] 
        private Cannon cannon;

        private bool _useCalculation = true;

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Unity events
        
        private void Update()
        {
            Quaternion targetRotation = Quaternion.Euler(
                new Vector3(_useCalculation ? -GetCannonAngle() : 0f, transform.eulerAngles.y, transform.eulerAngles.z));
            transform.rotation 
                = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            return;
            
            float GetCannonAngle()
            {
                Vector3 p0 = BezierCurve.GetPointOnCurve(cannon.StartPosition, cannon.EndPosition, cannon.MaxAltitude, 0f);
                Vector3 p1 = BezierCurve.GetPointOnCurve(cannon.StartPosition, cannon.EndPosition, cannon.MaxAltitude, 0.1f);
            
                Vector3 initialDirection = (p1 - p0).normalized;
                Vector3 flatDirection = new Vector3(initialDirection.x, 0, initialDirection.z).normalized;

                float angleRadians = Mathf.Acos(Vector3.Dot(initialDirection, flatDirection));
                return angleRadians * Mathf.Rad2Deg;
            }
        }

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        
        public void SetCalculatedRotation()
        {
            _useCalculation = true;
        }

        public void SetDefaultRotation()
        {
            _useCalculation = false;
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}