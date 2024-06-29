using Internal.Codebase.ObjectPool;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unit.Enemy.Base;
using UnityEngine;

namespace Base.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameWindowController gameWindowController;
        [SerializeField] private List<Level> levels = new List<Level>();
        [SerializeField] private Skeleton skeletonPrefab;
        [SerializeField] private Halk halkPrefab;
        [SerializeField] private Hammerman hammermanPrefab;

        private ObjectPool<Skeleton> skeletonPool;
        private ObjectPool<Halk> cyborgPool;
        private ObjectPool<Hammerman> hammermanPool;

        [SerializeField] private List<Enemy> enemies = new List<Enemy>();
        [SerializeField] private bool isLastWave = false;

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
            int currentLevel = Data.Data.Instance.CurrentLevel;
            int waveCount = levels[currentLevel].Waves.Count;

            for (int i = 0; i < waveCount; i++)
            {
                Wave wave = levels[currentLevel].Waves[i];

                StartCoroutine(Wave(wave.time, wave.enemies, i == waveCount - 1));
            }
        }

        private IEnumerator Wave(float time, List<Wave.EnemyCount> enemiesCount, bool isLastWave)
        {
            yield return new WaitForSeconds(time);

            foreach (Wave.EnemyCount enemy in enemiesCount)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    switch (enemy.Type)
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

            this.isLastWave = isLastWave;
        }

        private void Init<T>(T instance) where T : Enemy
        {
            enemies.Add(instance);
            float randomYPos = Random.Range(minStartPos, maxStartPos);
            instance.transform.position = new Vector3(10, randomYPos, 0);
            instance.Init(CheckWin);
        }

        public void CheckWin(Enemy enemy)
        {
            enemies.Remove(enemy);

            if (!isLastWave) return;

            if (enemies.Count == 0)
            {
                gameWindowController.Win();
            }
        }
    }
}
