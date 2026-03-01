using UnityEngine;

/// <summary>
/// 경험치 획득 및 레벨업 처리.
/// 적 사망 이벤트를 구독해서 자동으로 경험치를 쌓습니다.
/// </summary>
public class ExpSystem : MonoBehaviour
{
    [Header("레벨업 설정")]
    [SerializeField] private int _baseExpToLevel = 50;
    [SerializeField] private float _expScalePerLevel = 1.3f; // 레벨당 필요 경험치 증가율

    private int _currentExp;
    private int _expToNextLevel;
    private int _currentLevel = 1;

    public int CurrentExp => _currentExp;
    public int ExpToNextLevel => _expToNextLevel;
    public int CurrentLevel => _currentLevel;

    /// <summary>
    /// 현재 경험치 비율 (0~1). UI 게이지용.
    /// </summary>
    public float ExpRatio => (float)_currentExp / _expToNextLevel;

    private void Start()
    {
        _expToNextLevel = _baseExpToLevel;

        // 적 사망 이벤트 구독
        EventManager.Instance.Subscribe<int>("OnEnemyKilled", GainExp);
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance)
        {
            EventManager.Instance.Unsubscribe<int>("OnEnemyKilled", GainExp);
        }
    }

    private void GainExp(int amount)
    {
        _currentExp += amount;

        // 레벨업 체크 (한 번에 여러 레벨 오를 수 있음)
        while (_currentExp >= _expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _currentExp -= _expToNextLevel;
        _currentLevel++;

        // 다음 레벨 필요 경험치 증가
        _expToNextLevel = Mathf.RoundToInt(_baseExpToLevel * Mathf.Pow(_expScalePerLevel, _currentLevel - 1));

        // 레벨업 이벤트 발행 (나중에 스킬 선택 UI 등에서 구독)
        EventManager.Instance.Publish("OnLevelUp", _currentLevel);

        Debug.Log($"[ExpSystem] 레벨업! Lv.{_currentLevel} | 다음 레벨까지: {_expToNextLevel}");
    }
}