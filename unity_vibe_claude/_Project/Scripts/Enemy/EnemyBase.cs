using UnityEngine;

/// <summary>
/// 적 기본 클래스. 모든 적이 이 클래스를 상속받습니다.
/// 플레이어를 향해 이동하고, 체력이 0이 되면 풀에 반환됩니다.
/// </summary>
public class EnemyBase : MonoBehaviour, IPoolable
{
    [Header("스탯")]
    [SerializeField] protected float _maxHp = 10f;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected float _damage = 5f;
    [SerializeField] protected int _expAmount = 10;
    public float Damage => _damage;

    
    [Header("참조")]
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    protected float _currentHp;
    protected Transform _playerTransform;
    protected string _poolTag;

    

    private void Start()
    {
        // 플레이어 캐싱 (풀에서 재활용되므로 Start에서 한 번만)
        if (_playerTransform == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) _playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (_playerTransform == null) return;

        MoveTowardPlayer();
        UpdateFacing();
    }

    /// <summary>
    /// 플레이어를 향해 이동. 자식 클래스에서 오버라이드 가능.
    /// </summary>
    protected virtual void MoveTowardPlayer()
    {
        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        transform.position += (Vector3)(direction * _moveSpeed * Time.deltaTime);
    }

    private void UpdateFacing()
    {
        if (_playerTransform == null) return;

        // 플레이어가 오른쪽에 있으면 오른쪽 바라보기
        _spriteRenderer.flipX = _playerTransform.position.x < transform.position.x;
    }

    /// <summary>
    /// 데미지를 받습니다. 체력이 0 이하면 사망 처리.
    /// </summary>
    public virtual void TakeDamage(float damage)
    {
        _currentHp -= damage;

        if (_currentHp <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // 경험치 이벤트 발행
        EventManager.Instance.Publish("OnEnemyKilled", _expAmount);

        // 풀에 반환
        ObjectPoolManager.Instance.Release(_poolTag, gameObject);
    }

    /// <summary>
    /// 풀 태그 설정. Spawner에서 소환 시 호출.
    /// </summary>
    public void SetPoolTag(string tag)
    {
        _poolTag = tag;
    }

    /// <summary>
    /// 스탯 설정. 시간이 지날수록 적이 강해지는 용도.
    /// </summary>
    public void SetStats(float hp, float speed, float damage, int exp)
    {
        _maxHp = hp;
        _moveSpeed = speed;
        _damage = damage;
        _expAmount = exp;
    }

    // === IPoolable 구현 ===

    public void OnGetFromPool()
    {
        _currentHp = _maxHp;

        if (_playerTransform == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) _playerTransform = player.transform;
        }
    }

    public void OnReleaseToPool()
    {
        // 필요 시 이펙트, 사운드 정리
    }
}