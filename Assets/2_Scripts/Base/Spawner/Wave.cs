using System;
using System.Collections.Generic;
using Unit.Enemy.Base;

namespace Base.Spawner
{
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
