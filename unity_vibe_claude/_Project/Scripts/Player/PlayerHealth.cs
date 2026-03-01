using UnityEngine;

/// <summary>
/// 플레이어 체력 관리. 적과 접촉 시 데미지, HP 0이면 사망 이벤트 발행.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Header("체력")]
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _invincibleTime = 0.5f; // 피격 후 무적 시간

    private float _currentHp;
    private float _invincibleTimer;
    private bool _isDead;
    private SpriteRenderer _spriteRenderer;

    public float CurrentHp => _currentHp;
    public float MaxHp => _maxHp;
    public bool IsDead => _isDead;

    private void Start()
    {
        _currentHp = _maxHp;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 무적 시간 카운트
        if (_invincibleTimer > 0f)
        {
            _invincibleTimer -= Time.deltaTime;

            // 깜빡임 효과
            float alpha = Mathf.PingPong(Time.time * 10f, 1f) > 0.5f ? 1f : 0.3f;
            SetAlpha(alpha);
        }
        else
        {
            SetAlpha(1f);
        }
    }

    public void ModifyMaxHp(float multiplier)
    {
        _maxHp *= multiplier;
        _currentHp = _maxHp; // 최대체력 올리면 풀회복
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_isDead) return;
        if (_invincibleTimer > 0f) return;

        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                TakeDamage(enemy.Damage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;
        if (_invincibleTimer > 0f) return;

        _currentHp -= damage;
        _invincibleTimer = _invincibleTime;

        // 피격 이벤트
        EventManager.Instance.Publish("OnPlayerHit");

        if (_currentHp <= 0f)
        {
            _currentHp = 0f;
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        EventManager.Instance.Publish("OnPlayerDead");

        // 이동 멈추기
        var controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;

        // 무기 멈추기
        var weapon = GetComponent<AutoShootWeapon>();
        if (weapon != null) weapon.enabled = false;
    }

    public void Heal(float amount)
    {
        if (_isDead) return;
        _currentHp = Mathf.Min(_currentHp + amount, _maxHp);
    }

    private void SetAlpha(float alpha)
    {
        if (_spriteRenderer == null) return;
        Color c = _spriteRenderer.color;
        c.a = alpha;
        _spriteRenderer.color = c;
    }
}