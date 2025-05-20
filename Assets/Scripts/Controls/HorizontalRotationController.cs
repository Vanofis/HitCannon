using UnityEngine;

namespace Features.Controls
{
    public class HorizontalRotationController : MonoBehaviour
    {
        [SerializeField] 
        private Transform targetTransform;

        [Space] 
        [Min(0f)]
        [SerializeField] 
        private float rotationSpeed = 1.5f;
        [Min(0f)]
        [SerializeField]
        private float maxAngle = 30f;
        
        private float _targetProgress;
        private float _rotationProgress;

#if UNITY_EDITOR
        public Transform EditorTargetTransform => targetTransform;
        public float EditorMaxAngle => maxAngle;
        
        private void OnValidate()
        {
            targetTransform ??= transform;
        }
#endif
        
        private void Update()
        {
            if (Mathf.Approximately(_rotationProgress, _targetProgress))
            {
                targetTransform.rotation = Quaternion.Euler(0f, _targetProgress * maxAngle, 0f);
                return;
            }
            
            _rotationProgress = Mathf.MoveTowardsAngle(_rotationProgress, _targetProgress, rotationSpeed* Time.deltaTime);
            targetTransform.rotation = Quaternion.Euler(0f, _rotationProgress * maxAngle, 0f);
        }
        
        public void ResetTargetProgress()
        {
            _targetProgress = _rotationProgress;
        }
        
        public void SetTargetProgress(float newProgress)
        {
            _targetProgress = newProgress;
        }
    }
}