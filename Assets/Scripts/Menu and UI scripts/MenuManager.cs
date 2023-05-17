using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    public Button newGame;
    public Button selectLevel;
    public Button selectHero;
    public Button quit;
    
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartNewGame ()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("Level Selector");
    }
    public void SelectHero()
    {
        SceneManager.LoadScene("Hero Selector");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
