using UnityEngine;
using UnityEngine.Events;
using Features.Game;

namespace Features.Artillery
{
    public class TargetingModeSwitcher : MonoBehaviour, IResetable
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Data
        
        [HideInInspector]
        public bool canSwitch = true;
        
        [SerializeField] 
        private UnityEvent onTargetingModeEnabled = new();
        [SerializeField]
        private UnityEvent onTargetingModeDisabled = new();
        
        private bool _isTargetingMode;

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Public

        public void SwitchTargetingMode(bool state)
        {
            if (!canSwitch)
            {
                return;
            }
            
            _isTargetingMode = state;

            if (state)
            {
                onTargetingModeEnabled?.Invoke();
            }
            else
            {
                onTargetingModeDisabled?.Invoke();
            }
        }

        public void Switch(bool state)
        {
            this.canSwitch = state;
        }

        public void ResetObject()
        {
            SwitchTargetingMode(false);
            Switch(true);
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}