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
        StopCoroutine(_coroutine);
    }

    private IEnumerator Routine()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            yield return wait;

            int index = Random.Range(0, _spawnPoints.Count);

            if (_spawnPoints[index].TryGetComponent<SpawnPoint>(out SpawnPoint _point))
            {
                Enemy enemy = _point.GetUnit();
                enemy.Died += OnDied;
            }
        }
    }

    private void OnDied(Enemy enemy)
    {
        enemy.Died -= OnDied;
        enemy.SpawnPoint.Release(enemy);
    }
}
