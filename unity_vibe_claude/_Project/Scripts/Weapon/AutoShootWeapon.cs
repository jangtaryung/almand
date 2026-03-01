using UnityEngine;

/// <summary>
/// 가장 가까운 적을 자동으로 찾아 투사체를 발사하는 기본 무기.
/// </summary>
public class AutoShootWeapon : MonoBehaviour
{
    [Header("무기 스탯")]
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _fireInterval = 0.8f;
    [SerializeField] private float _detectRange = 8f;
    [SerializeField] private string _projectilePoolTag = "Projectile_Basic";

    private float _fireTimer;


    public void ModifyDamage(float multiplier)
    {
        _damage *= multiplier;
    }

    public void ModifyFireRate(float multiplier)
    {
        _fireInterval *= multiplier;
    }

    public void ModifyDetectRange(float multiplier)
    {
        _detectRange *= multiplier;
    }
    
    private void Update()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer >= _fireInterval)
        {
            TryShoot();
            _fireTimer = 0f;
        }
    }

    private void TryShoot()
    {
        Transform nearest = FindNearestEnemy();
        if (nearest == null) return;

        Vector2 direction = (nearest.position - transform.position).normalized;
        Vector3 spawnPos = transform.position;

        GameObject proj = ObjectPoolManager.Instance.Get(
            _projectilePoolTag,
            spawnPos,
            Quaternion.identity
        );

        if (proj != null)
        {
            var projectile = proj.GetComponent<Projectile>();
            projectile?.Init(direction, _damage, _projectilePoolTag);
        }
    }

    /// <summary>
    /// 감지 범위 내 가장 가까운 적을 찾습니다.
    /// </summary>
    private Transform FindNearestEnemy()
    {
        float closestDist = _detectRange;
        Transform closest = null;

        // Enemy 태그 오브젝트 전부 검색
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            if (!enemy.activeInHierarchy) continue;

            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }
    
}