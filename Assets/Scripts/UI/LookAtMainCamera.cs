using UnityEngine;

namespace Features.UI
{
    public class LookAtMainCamera : MonoBehaviour
    {
        private void Update()
        {
            if (!Camera.main)
            {
                return;
            }

            transform.LookAt(Camera.main.transform);
        }
    }
}