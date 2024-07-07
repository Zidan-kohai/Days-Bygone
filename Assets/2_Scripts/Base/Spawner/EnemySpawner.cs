using ObjectPool.Base;
using System.Collections;
using System.Collections.Generic;
using Unit.Enemy.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameWindowController gameWindowController;
        [SerializeField] private List<Level> levels = new List<Level>();
        [SerializeField] private List<Enemy> enemiesPrefab;
        [SerializeField] private Button nextLevel;


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



            if (Data.Data.Instance.CurrentLevel == levels.Count)
            {
                nextLevel.interactable = false;
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
                            Init(enemyPool.GetFree<Skeleton>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.Halk:
                            Init(enemyPool.GetFree<Halk>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.Hammerman:
                            Init(enemyPool.GetFree<Hammerman>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.Cyborg:
                            Init(enemyPool.GetFree<Cyborg>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.Goblin:
                            Init(enemyPool.GetFree<Goblin>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.MushroomMan:
                            Init(enemyPool.GetFree<MushroomMan>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.Neanderthal:
                            Init(enemyPool.GetFree<Neanderthal>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.Neanderthal2:
                            Init(enemyPool.GetFree<Neanderthal2>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.RedfGoblin:
                            Init(enemyPool.GetFree<RedfGoblin>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.Slime:
                            Init(enemyPool.GetFree<Slime>(), minStartPos, maxStartPos);
                            break;
                        case EnemyType.BigGoblin:
                            if(enemies.Count > 0) 
                            {
                                yield return new WaitForSeconds(2);
                                i--;
                                continue;
                            }
                            Init(enemyPool.GetFree<BigGoblin>(), minStartPos, 2.5f);
                            break;
                        case EnemyType.Demon:
                            if (enemies.Count > 0)
                            {
                                yield return new WaitForSeconds(2);
                                i--;
                                continue;
                            }
                            Init(enemyPool.GetFree<Demon>(), minStartPos, 2.5f);
                            break;
                        case EnemyType.Serber:
                            if (enemies.Count > 0)
                            {
                                yield return new WaitForSeconds(2);
                                i--;
                                continue;
                            }
                            Init(enemyPool.GetFree<Serber>(), minStartPos, 2.5f);
                            break;
                        case EnemyType.Worm:
                            if (enemies.Count > 0)
                            {
                                yield return new WaitForSeconds(2);
                                i--;
                                continue;
                            }
                            Init(enemyPool.GetFree<Worm>(), minStartPos, 2.5f);
                            break;
                        case EnemyType.Santa:
                            if (enemies.Count > 0)
                            {
                                yield return new WaitForSeconds(2);
                                i--;
                                continue;
                            }
                            Init(enemyPool.GetFree<Santa>(), minStartPos, 2.5f);
                            break;
                    }
                }
            }

            this.isLastWave = isLastWave;
        }

        private void Init<T>(T instance, float minStartPos, float maxStartPos) where T : Enemy
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
