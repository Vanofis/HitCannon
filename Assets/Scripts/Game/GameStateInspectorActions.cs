using UnityEngine;

namespace Features.Game
{
    public class GameStateInspectorActions : MonoBehaviour
    {
        public void OnTankReached()
        {
            GameState.CurrentState.OnTankReached();
        }
        
        public void OnTankDestroyed()
        {
            GameState.CurrentState.OnTankDestroyed();
        }
    }
}