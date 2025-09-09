using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform[] _waypoypoins;

    private int _pointIndex = 0;

    private void Update()
    {
        if (_waypoypoins[_pointIndex].transform.position == transform.position)
        {
            _pointIndex = (_pointIndex + 1) % _waypoypoins.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoypoins[_pointIndex].position, _speed * Time.deltaTime);
    }
}
