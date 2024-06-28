using Internal.Codebase.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using Unit.Enemy.Base;
using UnityEngine;

namespace Base.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Level> levels = new List<Level>();
        [SerializeField] private Skeleton skeletonPrefab;
        [SerializeField] private Halk halkPrefab;
        [SerializeField] private Hammerman hammermanPrefab;

        private ObjectPool<Skeleton> skeletonPool;
        private ObjectPool<Halk> cyborgPool;
        private ObjectPool<Hammerman> hammermanPool;

        private float minStartPos = -4, maxStartPos = 4;

        private void Start()
        {
            skeletonPool = new ObjectPool<Skeleton>(skeletonPrefab, transform, 5, true);
            cyborgPool = new ObjectPool<Halk>(halkPrefab, transform, 5, true);
            hammermanPool = new ObjectPool<Hammerman>(hammermanPrefab, transform, 5, true);

            InitWave();
        }

        private void InitWave()
        {
            foreach (var wave in levels[0].Waves)
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
                        case EnemyType.Halk:
                            Init(cyborgPool.GetFree());
                            break;
                        case EnemyType.Hammerman:
                            Init(hammermanPool.GetFree());
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
}
