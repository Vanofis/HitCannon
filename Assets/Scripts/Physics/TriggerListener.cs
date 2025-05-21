using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Physics
{
    [RequireComponent(typeof(Collider))]
    public class TriggerListener : MonoBehaviour
    {
        [SerializeField]
        private Collider trigger;
        
        [Space]
        [Header("Events")]
        [SerializeField]
        private UnityEvent onTriggerEnter = new();
        [SerializeField]
        private UnityEvent onEmptyTrigger = new();
        [SerializeField]
        private UnityEvent onTriggerExit = new();
        
        private bool _wasTriggered;
        private List<Collider> _enteredColliders = new();
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            trigger ??= GetComponent<Collider>();
            
            if (trigger)
            {
                trigger.isTrigger = true;
            }
        }
#endif

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
    }
}