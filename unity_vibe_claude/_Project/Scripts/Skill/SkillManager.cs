using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 레벨업 시 랜덤 스킬 3개를 제시하고, 선택된 스킬을 적용합니다.
/// </summary>
public class SkillManager : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private AutoShootWeapon _autoShootWeapon;

    private List<SkillData> _allSkills = new List<SkillData>();

    private void Start()
    {
        InitSkills();
        EventManager.Instance.Subscribe<int>("OnLevelUp", OnLevelUp);
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance)
            EventManager.Instance.Unsubscribe<int>("OnLevelUp", OnLevelUp);
    }

    private void InitSkills()
    {
        _allSkills.Add(new SkillData
        {
            id = "atk_up",
            skillName = "ATK Up",
            description = "Damage +20%",
            type = SkillData.SkillType.AttackUp
        });

        _allSkills.Add(new SkillData
        {
            id = "spd_up",
            skillName = "Speed Up",
            description = "Move Speed +15%",
            type = SkillData.SkillType.SpeedUp
        });

        _allSkills.Add(new SkillData
        {
            id = "fire_rate_up",
            skillName = "Fire Rate Up",
            description = "Attack Interval -15%",
            type = SkillData.SkillType.FireRateUp
        });

        _allSkills.Add(new SkillData
        {
            id = "max_hp_up",
            skillName = "Max HP Up",
            description = "Max HP +20%",
            type = SkillData.SkillType.MaxHpUp
        });

        _allSkills.Add(new SkillData
        {
            id = "heal_now",
            skillName = "Heal",
            description = "Recover 30% HP",
            type = SkillData.SkillType.HealNow
        });

        _allSkills.Add(new SkillData
        {
            id = "detect_up",
            skillName = "Range Up",
            description = "Detect Range +20%",
            type = SkillData.SkillType.DetectRangeUp
        });
    }

    private void OnLevelUp(int level)
    {
        // 랜덤 3개 뽑기
        List<SkillData> choices = GetRandomSkills(3);

        // UI에 선택지 보여주기
        EventManager.Instance.Publish("OnShowSkillChoice", choices);

        // 시간 멈추기
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 중복 없이 랜덤 스킬을 뽑습니다.
    /// </summary>
    public List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> pool = new List<SkillData>(_allSkills);
        List<SkillData> result = new List<SkillData>();

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int idx = Random.Range(0, pool.Count);
            result.Add(pool[idx]);
            pool.RemoveAt(idx);
        }

        return result;
    }

    /// <summary>
    /// 선택된 스킬을 실제로 적용합니다.
    /// </summary>
    public void ApplySkill(SkillData skill)
    {
        switch (skill.type)
        {
            case SkillData.SkillType.AttackUp:
                _autoShootWeapon.ModifyDamage(1.2f);
                break;

            case SkillData.SkillType.SpeedUp:
                float newSpeed = _playerController.GetMoveSpeed() * 1.15f;
                _playerController.SetMoveSpeed(newSpeed);
                break;

            case SkillData.SkillType.FireRateUp:
                _autoShootWeapon.ModifyFireRate(0.85f);
                break;

            case SkillData.SkillType.MaxHpUp:
                _playerHealth.ModifyMaxHp(1.2f);
                break;

            case SkillData.SkillType.HealNow:
                _playerHealth.Heal(_playerHealth.MaxHp * 0.3f);
                break;

            case SkillData.SkillType.DetectRangeUp:
                _autoShootWeapon.ModifyDetectRange(1.2f);
                break;
        }

        // 시간 다시 흐르게
        Time.timeScale = 1f;

        // 스킬 선택 완료 이벤트
        EventManager.Instance.Publish("OnSkillChosen");
    }
}