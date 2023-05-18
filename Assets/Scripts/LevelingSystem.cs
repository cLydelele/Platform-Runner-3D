using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelingSystem : MonoBehaviour
{
    public int characterLevel;
    public float currentXp;
    public float requiredXp;
    [Header ("Multipliers")]
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;
    private float lerpTimer;
    private float delayTimer;
    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        frontXpBar.fillAmount = currentXp/requiredXp;
        backXpBar.fillAmount = currentXp/requiredXp;
        requiredXp = CalculateRequiredXp();
        levelText.text = "Level " + characterLevel;
    }

    // Update is called once per frame
    void Update()
    {
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
    private void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = frontXpBar.fillAmount;

        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 2;
                percentComplete = percentComplete * percentComplete;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
        xpText.text = currentXp + "/" + requiredXp;
    }
    public void GainExperienceFlatRate(float xpGained)
    {
        currentXp += xpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    public void LevelUp()
    {
        characterLevel++;
        frontXpBar.fillAmount = 0;
        backXpBar.fillAmount = 0;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        requiredXp = CalculateRequiredXp();
        levelText.text = "Level " + characterLevel;
    }
    private int CalculateRequiredXp()
    {
        int solveForRequiredXp = 0;
        for (int levelCycle =1; levelCycle<=characterLevel;levelCycle++)
        {
            solveForRequiredXp = (int)Mathf.Floor(levelCycle+additionMultiplier*Mathf.Pow(powerMultiplier,levelCycle/divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }

    }
