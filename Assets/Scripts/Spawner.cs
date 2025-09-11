using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Min(0)] private float _spawnDelay = 2f;
    [SerializeField] private List<Transform> _spawnPoints;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Routine());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Routine()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (_spawnPoints.Count > 0 && enabled)
        {
            int index = Random.Range(0, _spawnPoints.Count);

            if (_spawnPoints[index].TryGetComponent(out SpawnPoint _point))
            {
                _point.SpawnEnemy();
            }

            yield return wait;
        }
    }
}
