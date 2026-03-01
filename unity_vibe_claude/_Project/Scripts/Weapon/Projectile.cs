using UnityEngine;

/// <summary>
/// 발사된 투사체. 적에게 닿으면 데미지를 주고 풀에 반환됩니다.
/// </summary>
public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 3f;

    private float _damage;
    private Vector2 _direction;
    private float _lifeTimer;
    private string _poolTag;

    private void Update()
    {
        // 이동
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);

        // 수명 체크
        _lifeTimer += Time.deltaTime;
        if (_lifeTimer >= _lifeTime)
        {
            ReturnToPool();
        }
    }

    /// <summary>
    /// 발사 초기화. WeaponBase에서 호출.
    /// </summary>
    public void Init(Vector2 direction, float damage, string poolTag)
    {
        _direction = direction.normalized;
        _damage = damage;
        _poolTag = poolTag;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        ObjectPoolManager.Instance.Release(_poolTag, gameObject);
    }

    // === IPoolable 구현 ===
    public void OnGetFromPool()
    {
        _lifeTimer = 0f;
    }

    public void OnReleaseToPool()
    {
        _direction = Vector2.zero;
    }
}