using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Config/Weapon", order = 0)]
public class Weapon : Item
{
    [field: SerializeField] public float AttackCooldown {  get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public AnimatorOverrideController Animator { get; private set; }
}
