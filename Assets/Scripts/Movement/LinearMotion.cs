using UnityEngine;

namespace Features.Movement
{
    public class LinearMotion : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Data
        
        [SerializeField] 
        private float moveSpeed = 3f;
        
        [Space]
        [SerializeField] 
        private float maxDistance = 27f;
        [SerializeField] 
        private float deadZone = 20f;

        private float _targetProgress;
        private float _currentProgress;
        
#if UNITY_EDITOR
        public float EditorCurrentProgress => _currentProgress;
        public float EditorMaxDistance => maxDistance;
        public float EditorDeadZone => deadZone;
#endif
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Unity events

        private void Awake()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, deadZone);
        }
        
        private void Update()
        {
            if (Mathf.Approximately(_currentProgress, _targetProgress))
            {
                return;
            }
            
            _currentProgress = Mathf.MoveTowards(_currentProgress, _targetProgress, moveSpeed * Time.deltaTime);
            transform.localPosition = 
                new Vector3(transform.localPosition.x, transform.localPosition.y, _currentProgress * maxDistance + deadZone);
        }
        
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        
        public void SetTargetProgress(float progress)
        {
            _targetProgress = Mathf.Clamp01(progress);
        }

        public void PauseProgression()
        {
            _targetProgress = _currentProgress;
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}