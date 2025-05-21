using Features.Game;
using UnityEngine;
using UnityEngine.Events;

namespace Features.Movement
{
    public class LinearMotion : MonoBehaviour, IResetable
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Data

        [Range(0f, 1f)]
        [SerializeField] 
        private float initialProgress;
        [SerializeField] 
        private float moveSpeed = 1f;
        [SerializeField] 
        private Vector3 motionDelta;
        private Vector3 _initialPosition;

        [Space] 
        [SerializeField] 
        private UnityEvent onMotionStart = new ();
        [SerializeField] 
        private UnityEvent onMotionEnd = new();
        
        private float _targetProgress;
        private float _currentProgress;

        private Vector3 CurrentDistance => _initialPosition + motionDelta * _currentProgress;
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Unity events

        private void Awake()
        {
            _targetProgress = initialProgress;
            _initialPosition = transform.localPosition;
        }
        
        private void Update()
        {
            if (Mathf.Approximately(_currentProgress, _targetProgress))
            {
                return;
            }
            
            _currentProgress = Mathf.MoveTowards(_currentProgress, _targetProgress, moveSpeed * Time.deltaTime);
            transform.localPosition = CurrentDistance;
            
            if (Mathf.Approximately(_currentProgress, _targetProgress))
            {
                onMotionEnd?.Invoke();
            }
        }
        
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public

        public void ResetObject()
        {
            _currentProgress = initialProgress;
            transform.localPosition = CurrentDistance;
            PauseProgression();
        }
        
        public void SetTargetProgress(float progress)
        {
            _targetProgress = Mathf.Clamp01(progress);
            onMotionStart?.Invoke();
        }

        public void PauseProgression()
        {
            _targetProgress = _currentProgress;
            onMotionEnd?.Invoke();
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}