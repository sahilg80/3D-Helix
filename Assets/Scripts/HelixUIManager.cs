using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelixUIManager : SingletonBehaviour<HelixUIManager>
{
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject gameWinPanel;
    [SerializeField]
    private TextMeshProUGUI textCurrentLevel;
    [SerializeField]
    private TextMeshProUGUI textNextLevel;
    [SerializeField]
    private Slider progressBar;
    public float CurrentLevelTotalRings { get; private set; }
    public int CurrentLevel { get; private set; }
    public float RingsPassed { get; private set; }
    private bool isGameOver;
    private bool isGameWin;

    public override void Awake()
    {
        base.Awake();
    }

    public int GetTotalLevelCleared()
    {
        CurrentLevel = PlayerPrefs.GetInt("LevelCleared", 0);
        return CurrentLevel;
    }

    public void ResetParameters(int totalRings)
    {
        isGameWin = false;
        isGameOver = false;
        RingsPassed = 0;
        CurrentLevelTotalRings = totalRings;
        CurrentLevel += 1;
        Time.timeScale = 1f;
        ResetGameUI();
    }

    // Start is called before the first frame update
    private void ResetGameUI()
    {
        textCurrentLevel.SetText(CurrentLevel.ToString());
        int nextLevel = CurrentLevel + 1;
        textNextLevel.SetText(nextLevel.ToString());
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        progressBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if((isGameWin || isGameOver) && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOverPanel()
    {
        isGameOver = true;
        isGameWin = false;
        Time.timeScale = 0f;  
        gameOverPanel.SetActive(true);
        AudioManager.Instance.PlaySound("GameOver");
    }

    public void GameWinPanel()
    {
        isGameOver = false;
        isGameWin = true;
        gameWinPanel.SetActive(true);
        AudioManager.Instance.PlaySound("GameWin");
        PlayerPrefs.SetInt("LevelCleared", CurrentLevel);
    }

    public void UpdateProgressBar()
    {
        RingsPassed += 1;
        Debug.Log("RingsPassed "+ RingsPassed+ "CurrentLevelTotalRings "+ CurrentLevelTotalRings);
        float value = RingsPassed / CurrentLevelTotalRings;
        Debug.Log(value);
        progressBar.value = value;
    }

}
