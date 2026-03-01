using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// 타이틀 화면 및 게임오버 Retry 처리.
/// </summary>
public class GameFlowUI : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private Button _startButton;

    [Header("GameOver")]
    [SerializeField] private Button _retryButton;

    private void Start()
    {
        Time.timeScale = 0f;
        _titlePanel.SetActive(true);

        _startButton.onClick.AddListener(OnStartGame);
        _retryButton.onClick.AddListener(OnRetry);

        // Retry 버튼은 게임오버 패널 안에 있으므로 처음엔 안 보임
    }

    private void OnStartGame()
    {
        _titlePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}