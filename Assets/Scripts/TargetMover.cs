using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private List<Transform> _wayPoints;

    private int _pointIndex = 0;

    private void Update()
    {
        if (_wayPoints[_pointIndex].transform.position == transform.position)
        {
            _pointIndex = ++_pointIndex % _wayPoints.Count;
        }

        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_pointIndex].position, _speed * Time.deltaTime);
    }
}
