using UnityEngine;

namespace Features.Game
{
    public class DestroyOnReset : MonoBehaviour, IResetable
    {
        public void ResetObject()
        {
            Destroy(gameObject);
        }
    }
}