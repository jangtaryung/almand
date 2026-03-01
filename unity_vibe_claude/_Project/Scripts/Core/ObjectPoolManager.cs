using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 범용 오브젝트 풀 매니저.
/// 프리팹 단위로 풀을 관리하며, 적·투사체·이펙트 등을 재활용합니다.
/// </summary>
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    /// <summary>
    /// 프리팹별 풀 설정. Inspector에서 세팅 가능.
    /// </summary>
    [System.Serializable]
    public class PoolConfig
    {
        public string tag;           // 풀 식별용 태그 (예: "Enemy_Bat", "Projectile_Fireball")
        public GameObject prefab;    // 풀링할 프리팹
        public int initialSize = 20; // 초기 생성 개수
    }

    [SerializeField]
    private List<PoolConfig> _poolConfigs = new List<PoolConfig>();

    // 태그별 오브젝트 큐
    private Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

    // 태그별 프리팹 레퍼런스 (풀 확장 시 사용)
    private Dictionary<string, GameObject> _prefabMap = new Dictionary<string, GameObject>();

    // 풀링된 오브젝트들의 부모 Transform (Hierarchy 정리용)
    private Dictionary<string, Transform> _parentMap = new Dictionary<string, Transform>();

    protected override void OnAwake()
    {
        InitializePools();
    }

    /// <summary>
    /// Inspector에 등록된 설정대로 풀을 초기화합니다.
    /// </summary>
    private void InitializePools()
    {
        foreach (var config in _poolConfigs)
        {
            CreatePool(config.tag, config.prefab, config.initialSize);
        }
    }

    /// <summary>
    /// 런타임에 새로운 풀을 동적으로 생성합니다.
    /// </summary>
    public void CreatePool(string tag, GameObject prefab, int initialSize)
    {
        if (_pools.ContainsKey(tag))
        {
            Debug.LogWarning($"[ObjectPool] '{tag}' 풀이 이미 존재합니다.");
            return;
        }

        _prefabMap[tag] = prefab;
        _pools[tag] = new Queue<GameObject>();

        // Hierarchy 정리용 부모 오브젝트 생성
        var parent = new GameObject($"Pool_{tag}");
        parent.transform.SetParent(transform);
        _parentMap[tag] = parent.transform;

        // 초기 오브젝트 생성
        for (int i = 0; i < initialSize; i++)
        {
            var obj = CreateNewObject(tag);
            obj.SetActive(false);
            _pools[tag].Enqueue(obj);
        }
    }

    /// <summary>
    /// 풀에서 오브젝트를 꺼냅니다. 풀이 비어있으면 자동으로 하나 더 만듭니다.
    /// </summary>
    public GameObject Get(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_pools.ContainsKey(tag))
        {
            Debug.LogError($"[ObjectPool] '{tag}' 풀이 존재하지 않습니다.");
            return null;
        }

        GameObject obj;

        if (_pools[tag].Count > 0)
        {
            obj = _pools[tag].Dequeue();
        }
        else
        {
            // 풀이 비었으면 자동 확장
            Debug.Log($"[ObjectPool] '{tag}' 풀 자동 확장. 초기 사이즈를 늘리는 걸 권장합니다.");
            obj = CreateNewObject(tag);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        // IPoolable 인터페이스가 있으면 초기화 호출
        var poolable = obj.GetComponent<IPoolable>();
        poolable?.OnGetFromPool();

        return obj;
    }

    /// <summary>
    /// 오브젝트를 풀에 반환합니다.
    /// </summary>
    public void Release(string tag, GameObject obj)
    {
        if (!_pools.ContainsKey(tag))
        {
            Debug.LogWarning($"[ObjectPool] '{tag}' 풀이 없어서 오브젝트를 파괴합니다.");
            Destroy(obj);
            return;
        }

        // IPoolable 인터페이스가 있으면 정리 호출
        var poolable = obj.GetComponent<IPoolable>();
        poolable?.OnReleaseToPool();

        obj.SetActive(false);
        _pools[tag].Enqueue(obj);
    }

    /// <summary>
    /// 특정 풀의 현재 대기 중인 오브젝트 수를 반환합니다.
    /// </summary>
    public int GetPoolCount(string tag)
    {
        return _pools.ContainsKey(tag) ? _pools[tag].Count : 0;
    }

    private GameObject CreateNewObject(string tag)
    {
        var obj = Instantiate(_prefabMap[tag], _parentMap[tag]);
        obj.name = $"{tag}_{obj.GetInstanceID()}";
        return obj;
    }
}