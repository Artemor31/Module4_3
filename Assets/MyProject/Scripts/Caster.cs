using UnityEngine;

public class Caster : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Spell _fireball;
    private Vector3 _target;

    public void CastFireball(Vector3 target)
    {
        _animator.SetTrigger("Casting");
        _target = target;
    }

    public void CastEvent()
    {
        var instance = Instantiate(_fireball, transform.position + Vector3.up, Quaternion.identity);
        instance.transform.LookAt(_target);
        instance.Construct(_target);
    }
}
