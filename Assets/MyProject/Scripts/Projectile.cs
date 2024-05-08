using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private float _damage;
    private LayerMask _layer;

    public void Construct(float speed, float damage, LayerMask layer)
    {
        _speed = speed;
        _damage = damage;
        _layer = layer;
    }

    private void Update() => transform.Translate(_speed * Time.deltaTime * Vector3.forward);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out var health) && other.TryGetComponent<Player>(out var _))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
