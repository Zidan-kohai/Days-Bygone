using UnityEngine;

namespace Unit.Enemy.Base
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Data/Config/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        public float Health;
        public float Speed;
        public float Damage;
        public float Reward;

    }
}

