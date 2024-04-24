using UnityEngine;

public abstract class Item : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public Slot Slot { get; private set; }
}

public enum Slot
{
    None = 0,
    Helmet = 1,
    Weapon = 2,
    Armor = 3,
    Boots = 4
}
