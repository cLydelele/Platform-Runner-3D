using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public SpriteRenderer playerImage;
    public int selectedOption = CharacterManager.instance.selectedOption;
    public float playerLevel;
    public float playerCurrentXp;
    public float playerRequiredXp;
    public static Player instance;

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
   

    public void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        playerImage.sprite = character.icon;
    }
    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
