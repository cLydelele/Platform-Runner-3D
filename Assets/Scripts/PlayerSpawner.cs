using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public Vector3 spawnPos = new Vector3(0, 0, -49f);
    public GameObject yourHero;
    public Player player;
    public int selectedOption;
    private void Start()
    {
        selectedOption = CharacterManager.instance.selectedOption;
        yourHero = characterDB.GetCharacter(selectedOption).characterObject;
        GetYourHero();
    }
    private void GetYourHero()
    {
        Instantiate(yourHero, spawnPos, gameObject.transform.rotation);
    }
}

 