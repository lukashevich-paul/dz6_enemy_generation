using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed = 2f;

    private Transform _targetTransform;
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action<Enemy> Died;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (_targetTransform != null)
            transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _targetTransform)
        {
            Died?.Invoke(this);
        }
    }

    public void Init(Vector3 startPosition, Transform targetTransform, Color color)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = startPosition;
        _targetTransform = targetTransform;
        _renderer.material.color = color;
    }
}
