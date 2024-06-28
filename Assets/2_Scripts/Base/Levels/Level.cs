using Base.Spawner;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelWave", menuName = "Data/Level/Level")]
public class Level : ScriptableObject
{
    public List<Wave> Waves = new List<Wave>();
}
