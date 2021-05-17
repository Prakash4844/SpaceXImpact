using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;

    public Text scoreText, bestText;
    public int score, bestScore;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestText.text = "Best: " + bestScore;
    }

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player Lives" + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    //Check for best Score
    //if Current score is Greater than best score 
    //best score = Current Score

    public void CheckForBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
            bestText.text = "Best: " + bestScore;
        }
    
    }



    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";
        score = 0;
    }

    //Resume game
    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    //Back to main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1;

    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }

}
