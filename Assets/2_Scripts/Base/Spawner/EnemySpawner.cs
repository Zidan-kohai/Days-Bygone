using ObjectPool.Base;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unit.Enemy.Base;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

namespace Base.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameWindowController gameWindowController;
        [SerializeField] private List<Level> levels = new List<Level>();
        [SerializeField] private List<Enemy> enemiesPrefab;

        private ObjectPool<Enemy> enemyPool;

        [SerializeField] private List<Enemy> enemies = new List<Enemy>();
        [SerializeField] private bool isLastWave = false;
        private bool isWined = false;

        private float minStartPos = -4, maxStartPos = 4;

        private void Start()
        {
            enemyPool = new ObjectPool<Enemy>(enemiesPrefab, transform, 3, true);

            InitWave();
        }

        private void InitWave()
        {
            int currentLevel = Data.Data.Instance.CurrentLevel - 1;
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
                            Init(enemyPool.GetFree<Skeleton>());
                            break;
                        case EnemyType.Halk:
                            Init(enemyPool.GetFree<Halk>());
                            break;
                        case EnemyType.Hammerman:
                            Init(enemyPool.GetFree<Hammerman>());
                            break;
                        case EnemyType.Cyborg:
                            Init(enemyPool.GetFree<Cyborg>());
                            break;
                        case EnemyType.Goblin:
                            Init(enemyPool.GetFree<Goblin>());
                            break;
                        case EnemyType.MushroomMan:
                            Init(enemyPool.GetFree<MushroomMan>());
                            break;
                        case EnemyType.Neanderthal:
                            Init(enemyPool.GetFree<Neanderthal>());
                            break;
                        case EnemyType.Neanderthal2:
                            Init(enemyPool.GetFree<Neanderthal2>());
                            break;
                        case EnemyType.RedfGoblin:
                            Init(enemyPool.GetFree<RedfGoblin>());
                            break;
                        case EnemyType.Slime:
                            Init(enemyPool.GetFree<Slime>());
                            break;
                        case EnemyType.BigGoblin:
                            Init(enemyPool.GetFree<BigGoblin>());
                            break;
                        case EnemyType.Demon:
                            Init(enemyPool.GetFree<Demon>());
                            break;
                        case EnemyType.Serber:
                            Init(enemyPool.GetFree<Serber>());
                            break;
                        case EnemyType.Worm:
                            Init(enemyPool.GetFree<Worm>());
                            break;
                        case EnemyType.Santa:
                            Init(enemyPool.GetFree<Santa>());
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

            if (isWined) return;

            if (!isLastWave) return;

            if (enemies.Count == 0)
            {
                isWined = true;
                gameWindowController.Win();
            }
        }
    }
}
