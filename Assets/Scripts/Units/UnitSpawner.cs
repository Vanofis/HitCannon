using System;
using System.Collections.Generic;
using Features.Game;
using UnityEngine;

namespace Features.Units
{
    public class UnitSpawner : MonoBehaviour, IResetable
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Data
        
        [Serializable]
        private struct RandomNumber
        {
            public float min;
            public float max;

            public float GetRandomNumber()
            {
                return UnityEngine.Random.Range(min, max);
            }
        }

        [SerializeField] private RandomNumber timeBeforeFirstSpawn;
        [SerializeField] private RandomNumber spawnInterval;

        [Space] 
        [SerializeField] 
        private float spawnRange = 2.5f;
        [SerializeField] 
        private List<GameObject> tanksToSpawn = new();

        private int _currentTankIndex = 0;
        private float _nextSpawnTimeStamp;

#if UNITY_EDITOR
        public float EditorSpawnRange => spawnRange;
#endif

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Unity events
        
        private void Awake()
        {
            _nextSpawnTimeStamp = Time.time + timeBeforeFirstSpawn.GetRandomNumber();
        }

        private void Update()
        {
            if (_currentTankIndex >= tanksToSpawn.Count || Time.time <= _nextSpawnTimeStamp)
            {
                return;
            }

            Instantiate(
                tanksToSpawn[_currentTankIndex++],
                transform.position + transform.right * (spawnRange * UnityEngine.Random.Range(-1f, 1f)),
                transform.rotation);

            _nextSpawnTimeStamp = Time.time + spawnInterval.GetRandomNumber();
        }
        
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        
        public void ResetObject()
        {
            _currentTankIndex = 0;
            _nextSpawnTimeStamp = Time.time + timeBeforeFirstSpawn.GetRandomNumber();
        }
        
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}