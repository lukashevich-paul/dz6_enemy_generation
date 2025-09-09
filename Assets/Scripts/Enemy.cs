using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed = 2f;

    private Vector3 _target;
    private Rigidbody _rigidbody;

    public event Action<Enemy> Died;

    public SpawnPoint Parent { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Target>(out _))
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            Died?.Invoke(this);
        }
    }

    public void SetParent(SpawnPoint parent)
    {
        Parent = parent;
    }

    public void Init(Vector3 startPosition, Vector3 target)
    {
        transform.position = startPosition;
        _target = target;
    }
}
