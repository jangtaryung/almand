/// <summary>
/// 스킬 업그레이드 정보를 담는 데이터 클래스.
/// </summary>
[System.Serializable]
public class SkillData
{
    public string id;
    public string skillName;
    public string description;
    public SkillType type;

    public enum SkillType
    {
        AttackUp,       // 공격력 증가
        SpeedUp,        // 이동속도 증가
        FireRateUp,     // 공격속도 증가
        MaxHpUp,        // 최대체력 증가
        HealNow,        // 즉시 회복
        DetectRangeUp   // 감지 범위 증가
    }
}