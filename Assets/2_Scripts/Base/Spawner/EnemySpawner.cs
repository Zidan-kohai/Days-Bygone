using Internal.Codebase.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using Unit.Enemy.Base;
using UnityEngine;

namespace Base.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves = new List<Wave>();
        [SerializeField] private Skeleton skeletonPrefab;
        [SerializeField] private Cyborg cyborgPrefab;

        private ObjectPool<Skeleton> skeletonPool;
        private ObjectPool<Cyborg> cyborgPool;

        private float minStartPos = -4, maxStartPos = 4;

        private void Start()
        {
            skeletonPool = new ObjectPool<Skeleton>(skeletonPrefab, transform, 10, true);
            cyborgPool = new ObjectPool<Cyborg>(cyborgPrefab, transform, 10, true);

            InitWave();
        }

        private void InitWave()
        {
            foreach (var wave in waves)
            {
                StartCoroutine(Wave(wave.time, wave.enemies));
            }
        }

        private IEnumerator Wave(float time, List<Wave.EnemyCount> enemiesCount)
        {
            yield return new WaitForSeconds(time);

            foreach (Wave.EnemyCount enemy in enemiesCount)
            {
                for(int i = 0; i < enemiesCount.Count; i++)
                {
                    switch(enemy.Type)
                    {
                        case EnemyType.Skeleton:
                            Init(skeletonPool.GetFree());
                            break;
                        case EnemyType.Cyborg:
                            Init(cyborgPool.GetFree());
                            break;
                    }
                }
            }
        }

        private void Init<T>(T instance) where T : Enemy
        {
            float randomYPos = UnityEngine.Random.Range(minStartPos, maxStartPos);
            instance.transform.position = new Vector3(10, randomYPos, 0);
            instance.Init();
        }
    }
        
    [Serializable]
    public class Wave
    {
        public float time;
        public List<EnemyCount> enemies;


        [Serializable]
        public class EnemyCount
        {
            public EnemyType Type;
            public int Count;
        }
    }
}
