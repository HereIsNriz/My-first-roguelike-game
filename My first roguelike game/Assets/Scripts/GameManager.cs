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
    public bool buttonPressed;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private GameObject upgradeSelectionPanel;
    [SerializeField] private GameObject[] upgradeButtons;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI enemyKilledText;
    [SerializeField] private TextMeshProUGUI levelReachedText;
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private BossController boss;
    [SerializeField] private PlayerMovement player;

    private int minutes;
    private int seconds;
    private int numOfButtonLocation = 3;
    private float yButtonLocation = -100f;
    private float xFirstButtonLocation = -500f;
    private float xMiddleButtonLocation = 0f;
    private float xLastButtonLocation = 500f;
    private bool upgradeButtonAppeared;
    private Vector2[] upgradeButtonLocation = new Vector2[3];

    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;
        buttonPressed = false;
        upgradeButtonAppeared = false;

        upgradeButtonLocation[0] = new Vector2(xFirstButtonLocation, yButtonLocation);
        upgradeButtonLocation[1] = new Vector2(xMiddleButtonLocation, yButtonLocation);
        upgradeButtonLocation[2] = new Vector2(xLastButtonLocation, yButtonLocation);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAmount < 1 || boss.bossDead)
        {
            GameWin();
        }

        UpdateCountdown();
        UpgradeSelection();
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

    private void UpgradeSelection()
    {
        if (isGameRunning)
        {
            if (player.upgradeSelection)
            {
                upgradeSelectionPanel.gameObject.SetActive(true);
                Time.timeScale = 0;

                if (!upgradeButtonAppeared)
                {
                    for (int i = 0; i < numOfButtonLocation; i++)
                    {
                        int buttonIndex = Random.Range(0, upgradeButtons.Length);

                        Instantiate(upgradeButtons[buttonIndex], upgradeButtonLocation[i], Quaternion.identity);
                    }
                    upgradeButtonAppeared = true;
                }

                if (buttonPressed)
                {
                    upgradeSelectionPanel.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    player.upgradeSelection = false;
                    buttonPressed = false;
                    upgradeButtonAppeared = false;
                }
            }
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
        levelReachedText.text = $"Level Reached: {player.playerLevel}";
        timeLeftText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
        statsPanel.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
