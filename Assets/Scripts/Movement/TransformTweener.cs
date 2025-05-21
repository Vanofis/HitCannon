using System;
using DG.Tweening;
using UnityEngine;

namespace Features.Movement
{
    public class TransformTweener : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Data
        
        [SerializeField]
        private Ease ease = Ease.Linear;
        [SerializeField] 
        private float time = 10f;

        private Sequence _sequence;
        
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Unity events
        
        private void Awake()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }
        
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        
        public void TweenCameraToStart()
        {
            TweenCamera(_initialPosition, _initialRotation, () => transform.SetParent(null));
        }

        public void TweenCamera(Transform target)
        {
            TweenCamera(Vector3.zero, Quaternion.identity, () => transform.SetParent(target));
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Private
        
        private void TweenCamera(Vector3 targetPosition, Quaternion targetRotation, Action onStart = null)
        {
            _sequence?.Pause();
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Insert(0f, transform.DOLocalMove(targetPosition, time))
                .Insert(0f, transform.DOLocalRotateQuaternion(targetRotation, time))
                .OnStart(() => onStart?.Invoke())
                .SetEase(ease)
                .SetAutoKill(true);
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}