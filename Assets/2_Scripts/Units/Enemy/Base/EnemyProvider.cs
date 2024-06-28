using UnityEngine;

namespace Unit.Enemy.Base
{
    [CreateAssetMenu(fileName = "EnemyProvider", menuName = "Data/Provider/Enemy")]
    public  class EnemyProvider : ScriptableObject
    {
        public EnemyType Type;
        public Enemy EnemyPrefab;
    }
}

