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
    public Button unpause;
    public Button goToNextScene;
    private int score;
    public CharacterDatabase characterDB;
    public static GameManager instance;
 

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
    public void PauseFunction()
    {       
        restartButton.gameObject.SetActive(true);
        returnToMainMenu.gameObject.SetActive(true);
        unpause.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Unpause()
    {
        Time.timeScale = 1.0f;
        restartButton.gameObject.SetActive(false);
        returnToMainMenu.gameObject.SetActive(false);
        unpause.gameObject.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
   public void GoToNextScene()
    {
        Time.timeScale = 1.0f;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
      //  SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"),SceneManager.GetSceneByName("Level "+nextScene.ToString()));
      //  SceneManager.LoadScene("Level " + nextScene.ToString());
        SceneManager.LoadScene(nextScene);
        // SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextScene));
        goToNextScene.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnToMainMenu.gameObject.SetActive(false);
    }
}




