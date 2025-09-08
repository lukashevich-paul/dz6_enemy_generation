using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public readonly string Finish = "Finish";

    [SerializeField, Min(0)] private float _speed = 2f;
    [SerializeField] private Vector3 _direction;

    public event Action<Enemy> Died;

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(nameof(Finish)))
        {
            Died?.Invoke(this);
        }
    }

    public void Init(Vector3 startPosition, Vector3 direction)
    {
        transform.position = startPosition;
        _direction = direction;
    }
}
