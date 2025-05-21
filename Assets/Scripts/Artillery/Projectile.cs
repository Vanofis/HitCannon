using UnityEngine;
using Features.Tools;
using Features.Units;
using UnityEngine.Events;

namespace Features.Artillery
{
    public class Projectile : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Data
        
        [Min(0f)]
        [SerializeField] 
        private float damage = 1f;
        [Min(0f)]
        [SerializeField] 
        private float projectileSpeed = 25f;
        [Min(0f)]
        [SerializeField]
        private int trackSmoothness = 30;

        [Space] 
        [SerializeField] 
        private UnityEvent onHit = new();
        
        private float _currentProgress;
        
        private Vector3 _start;
        private Vector3 _end;
        private float _maxAltitude;
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Unity events
        
        private void Update()
        {
            if (_currentProgress >= trackSmoothness)
            {
                Explode(true);
                return;
            }
            
            transform.position = BezierCurve.GetPointOnCurve(_start, _end, _maxAltitude, _currentProgress/trackSmoothness);
            _currentProgress += Time.deltaTime * projectileSpeed;
        }

        private void OnTriggerEnter(Collider col)
        {
            Explode(false, col);
        }

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        
        public void Init(Vector3 start, Vector3 end, float maxAltitude)
        {
            _start = start;
            _end = end;
            _maxAltitude = maxAltitude;
        }
        
        private void Explode(bool decreaseDamage, Collider col = null)
        {
            onHit?.Invoke();
            
            if (col && col.gameObject.TryGetComponent(out Health health))
            {
                health.Damage(damage * (decreaseDamage ? 0.75f : 1f));
            }
            
            Destroy(gameObject);
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}