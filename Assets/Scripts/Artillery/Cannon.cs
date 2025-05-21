using UnityEngine;
using UnityEngine.Events;

namespace Features.Artillery
{
    public class Cannon : MonoBehaviour
    {
        [Min(0f)]
        [SerializeField]
        private float cannonCooldown = 3f;

        [Header("Events")] 
        [SerializeField] 
        private UnityEvent onShoot = new();
        [SerializeField]
        private UnityEvent onReloaded = new();
        [SerializeField] 
        private UnityEvent<float> onReloadPercentChanged = new();
        
        private float _shootTimeStamp;
        private bool _isReloaded;
        
        public void Shoot()
        {
            _shootTimeStamp = Time.time + cannonCooldown;
            _isReloaded = false;
            
            onShoot?.Invoke();
            print("pew");
            //Projectile logic here etc.
        }

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
    }
}