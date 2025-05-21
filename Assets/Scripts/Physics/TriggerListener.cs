using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Physics
{
    [RequireComponent(typeof(Collider))]
    public class TriggerListener : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Data
        
        [Header("Events")]
        [SerializeField]
        private UnityEvent onTriggerEnter = new();
        [SerializeField]
        private UnityEvent onEmptyTrigger = new();
        [SerializeField]
        private UnityEvent onTriggerExit = new();
        
        private bool _wasTriggered;
        private List<Collider> _enteredColliders = new();
        
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Trigger events
        
        private void OnTriggerEnter(Collider other)
        {
            _wasTriggered = true;
            
            onTriggerEnter?.Invoke();
            _enteredColliders.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExit?.Invoke();
            _enteredColliders.Remove(other);
        }

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Unity events
        
        private void LateUpdate()
        {
            _enteredColliders.RemoveAll(x => !x);

            if (_enteredColliders.Count != 0)
            {
                return;
            }

            if (!_wasTriggered)
            {
                return;
            }
            
            _wasTriggered = false;
            onEmptyTrigger?.Invoke();
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}