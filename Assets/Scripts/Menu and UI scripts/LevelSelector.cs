using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

[DefaultExecutionOrder(1000)]

public class LevelSelector : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelText;
    public CharacterDatabase characterDB;


    // Start is called before the first frame update
    void Start()
    {
    }
    public void LoadLevel()
    {

        SceneManager.LoadScene("Level " + level.ToString());
        Time.timeScale = 1.0f;

        
       
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}




