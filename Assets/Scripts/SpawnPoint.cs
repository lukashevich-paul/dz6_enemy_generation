using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Renderer))]
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _target;

    private ObjectPool<Enemy> _pool;
    private Renderer _renderer;
    private int _defaultCapasity = 20;
    private int _maxSize = 20;

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

        _renderer = GetComponent<Renderer>();
    }

    public void SpawnEnemy()
    {
        Enemy enemy = _pool.Get();
        enemy.Init(transform.position, _target, _renderer.material.color);
        enemy.Died += Release;
    }

    public void Release(Enemy enemy)
    {
        enemy.Died -= Release;
        _pool.Release(enemy);
    }
}
