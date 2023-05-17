using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    public float speed = 20.0f;
    private float range = 30.0f;
    public Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (transform.position.z - playerPos.z > range)
        {
            Destroy(gameObject);
        }
    }
}
