using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Features.Game
{
    public class GameState : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Data

        [SerializeField] 
        private UnityEvent<string> onGameStart = new();
        [SerializeField] 
        private UnityEvent onGameWin = new();
        [SerializeField] 
        private UnityEvent onGameLose = new();
        [SerializeField] 
        private UnityEvent onGameEnd = new();
        [SerializeField] 
        private UnityEvent onReset = new();
        [SerializeField]
        private UnityEvent<string> onTanksDestroyedChanged = new();

        [Space]
        [Min(0)]
        [SerializeField] 
        private int tanksDestroyedRequired = 20;
        private int _currentTanksDestroyed = 0;
        
        public static GameState CurrentState { get; private set; }
        
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Unity events
        
        private void Awake()
        {
            CurrentState ??= this;
            onGameStart?.Invoke(tanksDestroyedRequired.ToString());
        }
        
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        
        public void OnTankReached()
        {
            onGameLose?.Invoke();
            onGameEnd?.Invoke();
        }
        
        public void OnTankDestroyed()
        {
            _currentTanksDestroyed++;
            _currentTanksDestroyed = Mathf.Clamp(_currentTanksDestroyed, 0, tanksDestroyedRequired);
            onTanksDestroyedChanged.Invoke(_currentTanksDestroyed.ToString());

            if (_currentTanksDestroyed == tanksDestroyedRequired)
            {
                onGameWin?.Invoke();
                onGameEnd?.Invoke();
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
            
            onReset?.Invoke();
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}