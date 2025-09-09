using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _target;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField, Min(0)] private float _spawnDelay = 2f;

    private Coroutine _coroutine;
    private ObjectPool<Enemy> _pool;

    private int _defaultCapasity = 40;
    private int _maxSize = 40;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapasity,
            maxSize: _maxSize
        );
    }

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
            int index = Random.Range(0, _spawnPoints.Count);
            Vector3 position = _spawnPoints[index].transform.position;

            Enemy enemy = _pool.Get();
            Vector3 direction = (_target.position - position).normalized;
            enemy.Init(position, direction);

            enemy.Died += OnDied;

            yield return wait;
        }
    }

    private void OnDied(Enemy enemy)
    {
        enemy.Died -= OnDied;

        _pool.Release(enemy);
    }
}
