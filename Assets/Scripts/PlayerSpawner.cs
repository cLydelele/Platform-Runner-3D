using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public Vector3 spawnPos = new Vector3(3, 3, -49f);
    public GameObject yourHero;
    public Player player;
    public int selectedOption;


    private void Start()
    {
        // testing instantiate for lvl desing
       // Instantiate(characterDB.character[1].characterObject, new Vector3(1f, 1f, -49f), gameObject.transform.rotation);
        //characterDB.LoadInfo();
        selectedOption = CharacterManager.instance.selectedOption;
        yourHero = characterDB.GetCharacter(selectedOption).characterObject;
        Instantiate(yourHero, spawnPos, gameObject.transform.rotation);
    }
}

 