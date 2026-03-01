using UnityEngine;

/// <summary>
/// 화면 밖 랜덤 위치에서 적을 소환합니다.
/// 시간이 지날수록 소환 주기가 빨라지고 적이 강해집니다.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("소환 설정")]
    [SerializeField] private string _enemyPoolTag = "Enemy_Basic";
    [SerializeField] private float _spawnInterval = 1.5f;
    [SerializeField] private float _minSpawnInterval = 0.3f;
    [SerializeField] private float _spawnDistanceFromPlayer = 12f;

    [Header("난이도 스케일링")]
    [SerializeField] private float _intervalDecreaseRate = 0.02f; // 초당 소환 간격 감소
    [SerializeField] private float _hpIncreaseRate = 0.5f;        // 초당 HP 증가

    private Transform _playerTransform;
    private float _spawnTimer;
    private float _elapsedTime;
    private float _baseHp = 10f;

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) _playerTransform = player.transform;
    }

    private void Update()
    {
        if (_playerTransform == null) return;

        _elapsedTime += Time.deltaTime;
        _spawnTimer += Time.deltaTime;

        // 시간에 따라 소환 간격 감소
        float currentInterval = Mathf.Max(
            _spawnInterval - (_intervalDecreaseRate * _elapsedTime),
            _minSpawnInterval
        );

        if (_spawnTimer >= currentInterval)
        {
            SpawnEnemy();
            _spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        GameObject enemy = ObjectPoolManager.Instance.Get(
            _enemyPoolTag,
            spawnPos,
            Quaternion.identity
        );

        if (enemy != null)
        {
            var enemyBase = enemy.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                enemyBase.SetPoolTag(_enemyPoolTag);

                // 시간에 따라 강해지는 스탯
                float scaledHp = _baseHp + (_hpIncreaseRate * _elapsedTime);
                enemyBase.SetStats(scaledHp, 2f, 5f, 10);
            }
        }
    }

    /// <summary>
    /// 플레이어 주변 원형 범위에서 랜덤 위치를 반환합니다.
    /// 화면 밖에서 소환되도록 일정 거리를 유지합니다.
    /// </summary>
    private Vector3 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(
            Mathf.Cos(angle) * _spawnDistanceFromPlayer,
            Mathf.Sin(angle) * _spawnDistanceFromPlayer,
            0f
        );
        return _playerTransform.position + offset;
    }
}