using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameRunning;
    public float timeAmount;
    public int enemyDeathCount;
    public int score;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI enemyKilledText;
    //[SerializeField] private TextMeshProUGUI levelReachedText;
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private BossController boss;

    private int minutes;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;

        timeAmount = 301.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAmount < 1 || boss.bossDead)
        {
            GameWin();
        }

        UpdateCountdown();
    }

    private void UpdateCountdown()
    {
        if (isGameRunning)
        {
            timeAmount -= Time.deltaTime;

            minutes = Mathf.FloorToInt(timeAmount / 60);
            seconds = Mathf.FloorToInt(timeAmount % 60);

            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void GameOver()
    {
        isGameRunning = false;
        gameOverPanel.gameObject.SetActive(true);
    }

    public void GameWin()
    {
        isGameRunning = false;
        gameWinPanel.gameObject.SetActive(true);
    }

    public void NextButton()
    {
        gameOverPanel.gameObject.SetActive(false);
        gameWinPanel.gameObject.SetActive(false);

        scoreText.text = $"Score: {score}";
        enemyKilledText.text = $"Enemy Killed: {enemyDeathCount}";
        // level
        timeLeftText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
        statsPanel.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
