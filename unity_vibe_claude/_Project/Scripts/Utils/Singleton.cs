using UnityEngine;

/// <summary>
/// Unity용 제네릭 싱글톤 베이스 클래스.
/// MonoBehaviour를 상속하며, 씬 간 유지 및 중복 인스턴스 자동 파괴를 지원합니다.
/// </summary>
/// <typeparam name="T">싱글톤으로 사용할 MonoBehaviour 타입</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    [Tooltip("true로 설정하면 씬 전환 시에도 인스턴스가 유지됩니다.")]
    private bool _dontDestroyOnLoad = true;

    private static T _instance;
    private static readonly object _lock = new object();

    /// <summary>
    /// 싱글톤 인스턴스. 접근 시점에 인스턴스가 없으면 씬에서 자동으로 찾습니다.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = FindFirstObjectByType<T>();

                    if (_instance == null)
                    {
                        Debug.LogError($"[Singleton] {typeof(T)} 인스턴스를 씬에서 찾을 수 없습니다.");
                    }
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// 인스턴스가 존재하는지 여부
    /// </summary>
    public static bool HasInstance => _instance != null;

    protected virtual void Awake()
    {
        lock (_lock)
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogWarning($"[Singleton] 중복된 {typeof(T)} 인스턴스를 파괴합니다. (게임오브젝트: {gameObject.name})");
                Destroy(gameObject);
                return;
            }

            _instance = this as T;

            if (_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            OnAwake();
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    /// <summary>
    /// 자식 클래스에서 초기화 로직을 구현할 때 오버라이드합니다.
    /// Awake에서 싱글톤 설정 후 호출됩니다.
    /// </summary>
    protected virtual void OnAwake() { }
}
