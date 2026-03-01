using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 게임 HUD. HP바, 경험치바, 레벨, 생존 타이머, 킬 카운트 표시.
/// </summary>
public class GameUI : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Image _hpFill;

    [Header("경험치")]
    [SerializeField] private Image _expFill;
    [SerializeField] private TextMeshProUGUI _levelText;

    [Header("게임 정보")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _killCountText;

    [Header("게임오버")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverTimeText;
    [SerializeField] private TextMeshProUGUI _gameOverKillText;

    [Header("참조")]
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private ExpSystem _expSystem;

    private float _survivalTime;
    private int _killCount;
    private bool _isGameOver;

    private void Start()
    {
        if (_gameOverPanel != null)
            _gameOverPanel.SetActive(false);

        // 킬 카운트 구독
        EventManager.Instance.Subscribe<int>("OnEnemyKilled", OnEnemyKilled);
        EventManager.Instance.Subscribe("OnPlayerDead", OnPlayerDead);
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance)
        {
            EventManager.Instance.Unsubscribe<int>("OnEnemyKilled", OnEnemyKilled);
            EventManager.Instance.Unsubscribe("OnPlayerDead", OnPlayerDead);
        }
    }

    private void Update()
    {
        if (_isGameOver) return;

        _survivalTime += Time.deltaTime;

        UpdateHpBar();
        UpdateExpBar();
        UpdateTimer();
    }

    private void UpdateHpBar()
    {
        if (_hpFill == null || _playerHealth == null) return;
        _hpFill.fillAmount = _playerHealth.CurrentHp / _playerHealth.MaxHp;
    }

    private void UpdateExpBar()
    {
        if (_expFill == null || _expSystem == null) return;
        _expFill.fillAmount = _expSystem.ExpRatio;

        if (_levelText != null)
            _levelText.text = $"Lv.{_expSystem.CurrentLevel}";
    }

    private void UpdateTimer()
    {
        if (_timerText == null) return;
        int min = Mathf.FloorToInt(_survivalTime / 60f);
        int sec = Mathf.FloorToInt(_survivalTime % 60f);
        _timerText.text = $"{min:00}:{sec:00}";
    }

    private void OnEnemyKilled(int exp)
    {
        _killCount++;
        if (_killCountText != null)
            _killCountText.text = $"Kill: {_killCount}";
    }

    private void OnPlayerDead()
    {
        _isGameOver = true;

        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);

            if (_gameOverTimeText != null)
            {
                int min = Mathf.FloorToInt(_survivalTime / 60f);
                int sec = Mathf.FloorToInt(_survivalTime % 60f);
                _gameOverTimeText.text = $"Time: {min:00}:{sec:00}";
            }

            if (_gameOverKillText != null)
                _gameOverKillText.text = $"Kills: {_killCount}";
        }

        // 게임 멈추기
        Time.timeScale = 0f;
    }
}