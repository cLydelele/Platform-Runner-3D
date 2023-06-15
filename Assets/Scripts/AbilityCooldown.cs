using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldown : MonoBehaviour
{
    public string abilityButtonAccesName = "Q";
    public Image darkMask;
    public TextMeshProUGUI cooldownTextDisplay;
    public Image myButtonImage;
    public float cooldownDuration;
    public float nextReadyTime;
    public float cooldownTimeLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AbilityReady()
    {
        cooldownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }
    public void CoolDown()
    {
        cooldownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(cooldownTimeLeft);
        cooldownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = (cooldownTimeLeft / cooldownDuration);
    }
    public void ButtonTriggered()
    {
        nextReadyTime = cooldownDuration + Time.time;
        cooldownTimeLeft = cooldownDuration;
        darkMask.enabled = true;
        cooldownTextDisplay.enabled = true;

    }
   
    // Update is called once per frame
    void Update()
    {
        bool CoolDownComplete = (Time.time > nextReadyTime);
        if (CoolDownComplete)
        {
            //   AbilityReady();
            if (Input.GetButtonDown("Q"))
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }
}
