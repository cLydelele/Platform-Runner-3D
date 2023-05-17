using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float xRange = 10.0f;
    public float zRange = -49.0f;
    public GameObject projectilePrefab;
    private Rigidbody playerRB;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //projectile launch
        if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale != 0.0f)
        {
             projectilePrefab.transform.position = transform.position + new Vector3(3.0f, 0, 0);
             Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

        }

        //movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);     
        KeepInBound();
        //jump
        if (Input.GetKeyDown(KeyCode.Space)&&isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        HasPowerup();
        powerupIndicator.transform.position = transform.position + new Vector3(0, 5.0f, 0);
       

    }
    public void KeepInBound()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z < zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }
    //powerup effect
    private void HasPowerup()
    {
        if (hasPowerup)
        {
            playerRB.mass = 2.0f;
        }
        else
        {
            playerRB.mass = 4.0f;
        }
    }

   //preventing double jump
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
      
    }
    //getting powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);

    }
   
}
