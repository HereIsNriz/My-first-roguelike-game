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

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI enemyKilledText;
    // playTimeText
    // scoreText
    [SerializeField] private TextMeshProUGUI countdownText;    

    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;

        timeAmount = 301.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCountdown();

        if (timeAmount <= 0)
        {
            // GameWin();
        }
    }

    private void UpdateCountdown()
    {
        if (isGameRunning)
        {
            timeAmount -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeAmount / 60);
            int seconds = Mathf.FloorToInt(timeAmount % 60);

            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        isGameRunning = false;
        gameOverPanel.gameObject.SetActive(true);
    }

    public void NextButton()
    {
        gameOverPanel.gameObject.SetActive(false);
        statsPanel.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
