using UnityEngine;

namespace Features.Movement
{
    public class AngularRotation : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Data
        
        [SerializeField] 
        private Transform directionTransform;

        [Space] 
        [Min(0f)]
        [SerializeField] 
        private float rotationSpeed = 1.5f;
        [Min(0f)]
        [SerializeField]
        private float maxAngle = 30f;
        
        private float _targetProgress;
        private float _currentProgress;
        private float _initialRotationSpeed;
        
#if UNITY_EDITOR
        public Transform EditorDirectionTransform => directionTransform;
        
        public float EditorMaxAngle => maxAngle;
        
        private void OnValidate()
        {
            directionTransform ??= transform;
        }
#endif
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Unity events
        
        private void Awake()
        {
            _initialRotationSpeed = rotationSpeed;
            directionTransform.localRotation = Quaternion.identity;
        }
        
        private void Update()
        {
            if (Mathf.Approximately(_currentProgress, _targetProgress))
            {
                return;
            }
            
            _currentProgress = Mathf.MoveTowardsAngle(_currentProgress, _targetProgress, rotationSpeed* Time.deltaTime);
            directionTransform.localRotation = Quaternion.Euler(directionTransform.localEulerAngles.x, 
                _currentProgress * maxAngle, directionTransform.localEulerAngles.z);
        }

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Public
        
        public void SetRotationSpeed(float newSpeed)
        {
            rotationSpeed = Mathf.Abs(newSpeed);
        }

        public void ResetRotationSpeed()
        {
            rotationSpeed = _initialRotationSpeed;
        }
        
        public void SetTargetProgress(float newProgress)
        {
            _targetProgress = Mathf.Clamp(newProgress, -1f, 1f);
        }
        
        public void PauseProgression()
        {
            _targetProgress = _currentProgress;
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}