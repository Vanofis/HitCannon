using System;
using UnityEngine;
using Features.Tools;
using Features.Units;

namespace Features.Artillery
{
    public class Projectile : MonoBehaviour
    {
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
        private GameObject particlesPrefab;
        
        private float _currentProgress;
        
        private Vector3 _start;
        private Vector3 _end;
        private float _maxAltitude;
        
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

        private void OnDestroy()
        {
            Instantiate(particlesPrefab, transform.position, Quaternion.identity);
        }

        public void Init(Vector3 start, Vector3 end, float maxAltitude)
        {
            _start = start;
            _end = end;
            _maxAltitude = maxAltitude;
        }
        
        private void Explode(bool decreaseDamage, Collider col = null)
        {
            if (col && col.gameObject.TryGetComponent(out Health health))
            {
                health.Damage(damage * (decreaseDamage ? 0.75f : 1f));
            }
            
            Destroy(gameObject);
        }
    }
}