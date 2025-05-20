using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace Features.Controls
{
    public class TurretRotationController : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] 
        private InputAction touchInputAction;
        [SerializeField]
        private Transform turretTransform;
        [SerializeField] 
        private float deadZoneRadius = 10f;
        
        [Space]
        [Header("Raycast settings")]
        [SerializeField]
        private float raycastMaxDistance = 1000f;
        [SerializeField]
        private LayerMask raycastLayerMask = 1;
        
        [Space]
        [Header("Tween settings")]
        [SerializeField]
        private Ease ease = Ease.OutCirc;
        [SerializeField] 
        private float rotationSpeed = 50f;
        
        private Tweener _tweener;
        
#if UNITY_EDITOR
        public Vector3 EditorTurretPosition => turretTransform?.position ?? Vector3.zero;
        public float EditorDeadZoneRadius => deadZoneRadius;
#endif
        
        private void OnEnable()
        {
            touchInputAction.performed += OnTouch;
            touchInputAction.Enable();
        }

        private void OnDisable()
        {
            touchInputAction.performed -= OnTouch;
            touchInputAction.Disable();
        }

        private void OnTouch(InputAction.CallbackContext context)
        {
            if (!Camera.main)
            {
                Debug.LogError("No main was camera found!");
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
            if (!Physics.Raycast(ray, out RaycastHit hit, raycastMaxDistance, raycastLayerMask))
            {
                return;
            }

            Vector2 turretPosition = new(turretTransform.position.x, turretTransform.position.z);
            Vector2 hitPosition = new(hit.point.x, hit.point.z);
            if (Vector2.Distance(turretPosition, hitPosition) <= deadZoneRadius)
            {
                return;
            }
            
            Quaternion targetRotation = Quaternion.LookRotation(hit.point - turretTransform.position);
            Vector3 targetRotationEuler = new(0f, targetRotation.eulerAngles.y, 0f);
            
            _tweener?.Pause();
            _tweener?.Kill();
            _tweener = turretTransform
                .DORotate(targetRotationEuler, rotationSpeed)
                .SetEase(ease)
                .SetSpeedBased(true)
                .SetAutoKill(true);
        }
    }
}