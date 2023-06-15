using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelingSystem : MonoBehaviour
{
    public float currentXp;
    public float currentLevel;
    public float requiredXp;
    public float MAX_LEVEL = 20;
    public CharacterDatabase characterDB;
    [Header ("Multipliers")]
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;
    public float lerpTimer;
    public float delayTimer;
    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;
    public GameObject[] enemy;
    public static LevelingSystem instance;
    

    // Start is called before the first frame update
    void Start()
    {
        LoadInfoFromDB();
        requiredXp = CalculateRequiredXp();       
        frontXpBar.fillAmount = currentXp /requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;
        levelText.text = "Level " + currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        SaveInfoToDB();
        UpdateXpUI();            
        if (Input.GetKeyDown(KeyCode.Equals))
            {
            GainExperienceFlatRate(20);
            }
        if (currentXp > requiredXp)
        {
             LevelUp();

        }
      
    }
  
   
    public void LoadInfoFromDB()
    {
        currentXp = characterDB.character[CharacterManager.instance.selectedOption].heroCurrentXp;
        currentLevel = characterDB.character[CharacterManager.instance.selectedOption].heroLevel;
       
    }
    public void SaveInfoToDB()
    {
        characterDB.character[CharacterManager.instance.selectedOption].heroLevel = currentLevel;
        characterDB.character[CharacterManager.instance.selectedOption].heroCurrentXp = currentXp;
        characterDB.character[CharacterManager.instance.selectedOption].heroRequiredXp = requiredXp;
    }
    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = frontXpBar.fillAmount;

        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 1)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 2;
                percentComplete = percentComplete * percentComplete;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
        xpText.text = currentXp + "/" + requiredXp;
    }
   //test method
    public void GainExperienceFlatRate(int xpGained)
    {
        
            currentXp += xpGained;
            lerpTimer = 0f;
            delayTimer = 0f;
        
    }
    public void LevelUp()
    {
        currentLevel++;
        frontXpBar.fillAmount = 0;
        backXpBar.fillAmount = 0;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        requiredXp = CalculateRequiredXp();
        levelText.text = "Level " + currentLevel;
        CharacterManager.instance.SettingCharactersModifiers();
    }

    private int CalculateRequiredXp()
    {
        int solveForRequiredXp = 0;
        for (int levelCycle =1; levelCycle<=currentLevel;levelCycle++)
        {
            solveForRequiredXp = (int)Mathf.Floor(levelCycle+additionMultiplier*Mathf.Pow(powerMultiplier,levelCycle/divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }

    }
