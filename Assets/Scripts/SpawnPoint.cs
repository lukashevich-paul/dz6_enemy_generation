using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _target;

    private ObjectPool<Enemy> _pool;
    private int _defaultCapasity = 40;
    private int _maxSize = 40;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => InstantiateEnemy(),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapasity,
            maxSize: _maxSize
        );
    }

    private Enemy InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_prefab);
        enemy.SetParent(this);

        return enemy;
    }

    public Enemy GetUnit()
    {
        Enemy enemy = _pool.Get();
        enemy.Init(transform.position, _target);

        return enemy;
    }

    public void Release(Enemy enemy)
    {
        _pool.Release(enemy);
    }
}
