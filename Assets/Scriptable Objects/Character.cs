using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[System.Serializable]
public class Character
{
    public string characterName;
    public GameObject characterObject;
    public Sprite icon;
    public float heroLevel;
    public float heroCurrentXp;
    public float heroRequiredXp;
    [Header("Modifiers")]
    public float speedModifier;
    public float jumpForceModifier;
    public float gravityModifier;
    public bool doubleJump;
    public float projectileCoolDown;
    public bool secondWind;

}
