using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    private GameManager gameManager;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (CompareTag("Enemy"))
            {
            Destroy(gameObject);
            Destroy(other.gameObject); 
            }
        gameManager.UpdateScore(pointValue);


    }
}
