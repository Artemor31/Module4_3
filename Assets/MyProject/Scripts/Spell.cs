using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private ParticleSystem _vfx;
    private Vector3 _target;
    private Collider[] _hits = new Collider[5];

    public void Construct(Vector3 target)
    {
        _target = target;
    }

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);

        if (Vector3.Distance(_target, transform.position) < 0.5f)
        {
            Destroy(gameObject);
            var vfx =Instantiate(_vfx, transform.position, Quaternion.identity);
            vfx.Play();
            DoDamage();
        }
    }

    private void DoDamage()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _hits, _layer);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
