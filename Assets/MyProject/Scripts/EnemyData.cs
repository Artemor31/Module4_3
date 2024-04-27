using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Config/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public EnemyBrain Prefab;
    public Weapon Weapon;
    public float StartHealth;
    public int Exp;
    public int Gold;
    public List<Item> Loot;
}
