using UnityEngine;

[CreateAssetMenu(fileName = "Role", menuName = "Config/Role", order = 0)]
public class Role : ScriptableObject
{
    public Weapon Weapon;
    public float StartHealth;
}
