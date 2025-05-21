using UnityEngine;

namespace Features.Tools
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        public void SpawnPrefabInstance(Transform spawnTransform)
        {
            Instantiate(prefab, spawnTransform.position, spawnTransform.rotation);
        }
    }
}