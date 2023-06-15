using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingProjectile : MonoBehaviour
{
    public float speed = 20.0f;
    private float range = 30.0f;
    public Vector3 playerPos;
    public Rigidbody projectileRB;



    // Start is called before the first frame update
    void Start()
    {
        projectileRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
       // projectileRB.AddForce(Vector3.forward*speed, ForceMode.Acceleration);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.z - playerPos.z > range)
        {
            Destroy(gameObject);
        }

    }

}
