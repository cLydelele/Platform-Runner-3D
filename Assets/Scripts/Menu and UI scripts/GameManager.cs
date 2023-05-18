using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button returnToMainMenu;
    private int score;
    

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);     
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void UpdateScore (int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);       
        restartButton.gameObject.SetActive(true);
        returnToMainMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }   
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

 

 
