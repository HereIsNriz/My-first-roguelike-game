using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameRunning;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject statsPanel;

    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
