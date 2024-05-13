using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Config/Progression", order = 0)]
public class Progression : ScriptableObject
{
    [SerializeField] private List<LevelData> _levels;
    public IReadOnlyList<LevelData> Levels => _levels;

    public int MaxExpFor(int level)
    {
        LevelData data = _levels.FirstOrDefault(l => l.Level == level);
        if (data == null)
            throw new Exception("No data for level: " + level);

        return data.MaxExp;
    }
}

[Serializable]
public class LevelData
{
    public int Level;
    public int MaxExp;
    public int HealthBonus;
}