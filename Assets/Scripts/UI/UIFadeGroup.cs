using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Features.UI
{
    public class UIFadeGroup : MonoBehaviour
    {
        [Serializable]
        private class FadeGroup
        {
            public CanvasGroup canvasGroup;

            public UnityEvent onFadeIn; 
            public UnityEvent onFadeOut;
        }
        
        [SerializeField]
        private Ease ease = Ease.Linear;
        [SerializeField] 
        private float time = 1f;
        
        [Space]
        [SerializeField]
        private List<FadeGroup> groups;

        private Sequence _sequence;
        
        public void FadeGroups(int endValue)
        {
            endValue = (int)Mathf.Clamp01(endValue);
            
            _sequence?.Pause();
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .SetEase(ease)
                .SetAutoKill(false);

            foreach (var group in groups)
            {
                _sequence.Insert(0f, group.canvasGroup.DOFade(endValue, time));
                AddCallback(endValue, group);
            }
        }

        private void AddCallback(int endValue, FadeGroup group)
        {
            if (endValue == 1)
            {
                _sequence.onComplete += () => group.onFadeIn?.Invoke();
            }
            else
            {
                _sequence.onPlay += () => group.onFadeOut?.Invoke();
            }
        }
    }
}