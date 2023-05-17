using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    Rigidbody enemyRb;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPlayerDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookPlayerDirection * speed);
        
    }
}
