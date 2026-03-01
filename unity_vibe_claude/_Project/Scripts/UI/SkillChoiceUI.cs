using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 레벨업 시 스킬 3개를 보여주고 선택하는 UI.
/// </summary>
public class SkillChoiceUI : MonoBehaviour
{
    [Header("패널")]
    [SerializeField] private GameObject _panel;

    [Header("스킬 버튼 3개")]
    [SerializeField] private Button _btn0;
    [SerializeField] private Button _btn1;
    [SerializeField] private Button _btn2;

    [Header("스킬 이름 텍스트")]
    [SerializeField] private TextMeshProUGUI _nameText0;
    [SerializeField] private TextMeshProUGUI _nameText1;
    [SerializeField] private TextMeshProUGUI _nameText2;

    [Header("스킬 설명 텍스트")]
    [SerializeField] private TextMeshProUGUI _descText0;
    [SerializeField] private TextMeshProUGUI _descText1;
    [SerializeField] private TextMeshProUGUI _descText2;

    [Header("참조")]
    [SerializeField] private SkillManager _skillManager;

    private List<SkillData> _currentChoices;

    private void Start()
    {
        _panel.SetActive(false);

        EventManager.Instance.Subscribe<List<SkillData>>("OnShowSkillChoice", ShowChoices);
        EventManager.Instance.Subscribe("OnSkillChosen", HidePanel);

        _btn0.onClick.AddListener(() => OnSkillSelected(0));
        _btn1.onClick.AddListener(() => OnSkillSelected(1));
        _btn2.onClick.AddListener(() => OnSkillSelected(2));
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance)
        {
            EventManager.Instance.Unsubscribe<List<SkillData>>("OnShowSkillChoice", ShowChoices);
            EventManager.Instance.Unsubscribe("OnSkillChosen", HidePanel);
        }
    }

    private void ShowChoices(List<SkillData> choices)
    {
        _currentChoices = choices;
        _panel.SetActive(true);

        _nameText0.text = choices[0].skillName;
        _descText0.text = choices[0].description;

        _nameText1.text = choices[1].skillName;
        _descText1.text = choices[1].description;

        _nameText2.text = choices[2].skillName;
        _descText2.text = choices[2].description;
    }

    private void OnSkillSelected(int index)
    {
        _skillManager.ApplySkill(_currentChoices[index]);
    }

    private void HidePanel()
    {
        _panel.SetActive(false);
    }
}