using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public CharacterDatabase characterDB;
    public TextMeshProUGUI nameText;
    public SpriteRenderer playerImage;
    public int selectedOption=0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption); 
       
    }
   

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= characterDB.characterCount)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        Save();
    }
    public void BackOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = characterDB.characterCount - 1;
        }
        UpdateCharacter(selectedOption);
        Save();
    }
    public void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        nameText.text = character.characterName;
        playerImage.sprite = character.icon;
    }
    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
}
