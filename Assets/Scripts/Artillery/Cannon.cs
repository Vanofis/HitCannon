using UnityEngine;
using UnityEngine.Events;
using Features.Game;

namespace Features.Artillery
{
    public class Cannon : MonoBehaviour, IResetable
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Data
        
        [Min(0f)]
        [SerializeField]
        private float cannonCooldown = 3f;

        [Space] 
        [SerializeField] 
        private float maxAltitude;
        [SerializeField] 
        private Transform startPoint;
        [SerializeField]
        private Transform endPoint;
        [SerializeField]
        private GameObject projectilePrefab;
        
        [Header("Events")] 
        [SerializeField] 
        private UnityEvent onShoot = new();
        [SerializeField]
        private UnityEvent onReloaded = new();
        [SerializeField] 
        private UnityEvent<float> onReloadPercentChanged = new();
        
        private float _shootTimeStamp;
        private bool _isReloaded;
        
        public Vector3 StartPosition => startPoint.position;
        public Vector3 EndPosition => endPoint.position;
        public float MaxAltitude => maxAltitude;
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Unity events
        
        private void Update()
        {
            if (Time.time <= _shootTimeStamp)
            {
                onReloadPercentChanged?.Invoke(1f - (_shootTimeStamp - Time.time) / cannonCooldown);
            }
            else if (!_isReloaded)
            {
                onReloaded?.Invoke();
                _isReloaded = true;
            }
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        
        public void Shoot()
        {
            _shootTimeStamp = Time.time + cannonCooldown;
            _isReloaded = false;
            
            onShoot?.Invoke();
            Instantiate(projectilePrefab, startPoint.position, Quaternion.identity)
                .GetComponent<Projectile>()
                .Init(startPoint.position, endPoint.position, maxAltitude);
        }

        public void ResetObject()
        {
            _shootTimeStamp = Time.time;
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}