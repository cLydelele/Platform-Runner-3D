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
    public float playerLevel;
    public TextMeshProUGUI playerLevelDisplayed;
    
    // public float playerCurrentXp;
    //public float playerRequiredXp;
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
        selectedOption = 0;
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
        SettingCharactersModifiers();
    }
    void Update()
    {
        playerLevel = LevelingSystem.instance.currentLevel;  
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
        playerLevelDisplayed.text = "Level "+ character.heroLevel.ToString();
        
       // playerLevel = characterDB.character[selectedOption].heroLevel;
       // playerCurrentXp = characterDB.character[selectedOption].heroCurrentXp;
        //playerRequiredXp = characterDB.character[selectedOption].heroRequiredXp;
    }
    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");

    }
    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);

    }
        public void SettingCharactersModifiers()
        {
            // character one modifiers
            if (characterDB.character[0].heroLevel == 5)
            {
                characterDB.character[0].doubleJump = true;
            }
            else { characterDB.character[0].doubleJump = false; }
            if (characterDB.character[0].heroLevel == 10)
            {
                characterDB.character[0].speedModifier = 1.5f;
            }
            else { characterDB.character[0].speedModifier = 1.0f; }
            // character two modifiers
            if (characterDB.character[1].heroLevel == 5)
            {
                characterDB.character[1].projectileCoolDown = 1.2f;
            }
            else { characterDB.character[1].projectileCoolDown = 2.0f; }
            if (characterDB.character[1].heroLevel == 10)
            {
                characterDB.character[1].jumpForceModifier = 1.5f;
            }
            else { characterDB.character[1].jumpForceModifier = 1.0f; }
            // character three modifiers
            if (characterDB.character[2].heroLevel == 5)
            {
                characterDB.character[2].secondWind = true;
            }
            else { characterDB.character[2].secondWind = false; }
            if (characterDB.character[2].heroLevel == 10)
            {
                characterDB.character[2].gravityModifier = 0.7f;
            }
            else { characterDB.character[2].gravityModifier = 1.0f; }
        }
    }

