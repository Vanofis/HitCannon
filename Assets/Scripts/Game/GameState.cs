using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Features.Game
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] 
        private UnityEvent onGameWin;
        [SerializeField] 
        private UnityEvent onGameLose;

        [Space]
        [Min(0)]
        [SerializeField] 
        private int tanksDestroyedRequired = 20;
        private int _currentTanksDestroyed = 0;
        
        public static GameState CurrentState { get; private set; }

        private void Awake()
        {
            CurrentState ??= this;
        }

        public void OnTankReached()
        {
            onGameLose?.Invoke();
        }
        
        public void OnTankDestroyed()
        {
            _currentTanksDestroyed++;
            Debug.Log(_currentTanksDestroyed);

            if (_currentTanksDestroyed == tanksDestroyedRequired)
            {
                onGameWin?.Invoke();
            }
        }

        public void ResetGame()
        {
            _currentTanksDestroyed = 0;
            var resetables
                = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IResetable>();

            foreach (var resetable in resetables)
            {
                resetable.ResetObject();
            }
        }
    }
}