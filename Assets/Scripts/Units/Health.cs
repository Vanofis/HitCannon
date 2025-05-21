using UnityEngine;
using UnityEngine.Events;

namespace Features.Units
{
    public class Health : MonoBehaviour
    {
        [Min(0)] 
        [SerializeField] 
        private float maxHealth = 2;
        [Min(0)] 
        [SerializeField] 
        private float minHealth = 0f;
        
        [Space] 
        [SerializeField] 
        private UnityEvent<float> onHealthPercentChanged = new();
        [SerializeField] 
        private UnityEvent onDeath = new();

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
            onHealthPercentChanged?.Invoke(1f);
        }

        public void Damage(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Clamp(_currentHealth, minHealth, maxHealth);
            
            onHealthPercentChanged?.Invoke(_currentHealth/maxHealth);

            if (!(_currentHealth <= minHealth))
            {
                return;
            }
            
            onDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}