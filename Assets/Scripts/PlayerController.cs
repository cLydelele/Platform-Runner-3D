using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed=10.0f;
    public float xRange = 10.0f;
    public float zRange = -49.0f;
    public GameObject projectilePrefab;
    private Rigidbody playerRB;
    public float jumpForce=33.0f;
    public float gravityModifier=1.6f;
    public bool isOnGround = true;
    public bool hasPowerup;
    public bool canDoubleJump;
    public bool hasSecondWind;
    public GameObject powerupIndicator;
    private GameManager gameManager;
    public CharacterDatabase characterDB;
    public int selectedCharacter;
    public float characterSpeedModifier;    
    public float characterJumpForceModifier;
    public float characterGravityModifier;
    public bool characterDoubleJump;
    public bool projectileIsAvailable = true;
    public float characterCoolDownDuration;
    public bool characterSecondWind;
    public GameObject shootingPoint;
    public Animator animator;
    public bool isRunning;
    public bool isJumping;
    public bool isJumpingWhileRunning;
    public Joystick joystick;
    public Button jumpButton;
    public Button shootButton;
    public float joyTest;
    

    void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetCharacterModifiers();
        SetCharacterModifiers();       
        Physics.gravity *= gravityModifier;
       
        projectileIsAvailable = true;
    }

    void HandleAnimations() 
    {
        isRunning=animator.GetBool("isRunning");
        isJumping=animator.GetBool("isJumping");
        isJumpingWhileRunning= animator.GetBool("isJumpingWhileRunning");
        if (verticalInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
      if (isRunning&&!isOnGround)
        {
            animator.SetBool("isJumpingWhileRunning", true);
        }

    }
    // Update is called once per frame
    void Update()
    {
       
        HandleAnimations();
        CheckPlayerPositionForEnd();
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.PauseFunction();
        }
        if (characterDB.character[selectedCharacter].heroLevel ==5) 
        {
            GetCharacterModifiers();
            SetCharacterModifiers();
        }
        if (characterDB.character[selectedCharacter].heroLevel == 10)
        {
            GetCharacterModifiers();
            SetCharacterModifiers();
        }
        //projectile launch
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootProjectile();
            
        }

        //movement by a joystick

        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;

        // editor wsad movement
       // horizontalInput = Input.GetAxis("Horizontal");
       // verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
  
        transform.Rotate(0, horizontalInput * 60 * Time.deltaTime, 0);

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
           Jumping();
        }
       
        HasPowerup();
        powerupIndicator.transform.position = transform.position + new Vector3(0, 5.0f, 0);
       

    }
    public void Jumping()
    {
        if (isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            canDoubleJump = true;
            animator.SetBool("isJumping", true);
        }
        else
        {
            if (canDoubleJump)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = false;
            }
        }
    }
    
    public void KeepInBound() //used in first itteration, when path was straight
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isJumpingWhileRunning", false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (characterDB.character[selectedCharacter].secondWind == false)
            {
                gameManager.GameOver();
            }
            else 
            {
                Cooldown();
                characterDB.character[selectedCharacter].secondWind = false;
                gameManager.gameOverText.gameObject.SetActive(false);
                gameManager.restartButton.gameObject.SetActive(false);
                gameManager.returnToMainMenu.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
            
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
    public void GetCharacterModifiers()
    {
        selectedCharacter = CharacterManager.instance.selectedOption;
        characterSpeedModifier = characterDB.character[selectedCharacter].speedModifier;
        characterJumpForceModifier = characterDB.character[selectedCharacter].jumpForceModifier;
        characterGravityModifier = characterDB.character[selectedCharacter].gravityModifier;
        characterDoubleJump = characterDB.character[selectedCharacter].doubleJump;
        characterCoolDownDuration = characterDB.character[selectedCharacter].projectileCoolDown;
        characterSecondWind = characterDB.character[selectedCharacter].secondWind;
}
   public void SetCharacterModifiers()
    {
            speed *= characterSpeedModifier;
            gravityModifier *= characterGravityModifier;
            jumpForce *= characterJumpForceModifier;
        
    }


    public void ShootProjectile()
    {
        if (Time.timeScale != 0.0f && projectileIsAvailable == true)
        {
            projectilePrefab.transform.position = new Vector3(0f, 1.4f, 0.4f) + transform.position;
            Instantiate(projectilePrefab, projectilePrefab.transform.position, this.gameObject.transform.rotation);
            projectileIsAvailable = false;
            Cooldown();
        }
    }
    public void Cooldown()
    {
    StartCoroutine(StartCooldown());
    }
    public IEnumerator StartCooldown()
    {      
        yield return new WaitForSeconds(characterCoolDownDuration);
        projectileIsAvailable = true;
    }
    public void CheckPlayerPositionForEnd()
    {
        int zMax = 537;
        int zMin = 526;
        int xMax = -27;
        int xMin = -43;
        if (transform.position.x > xMin && transform.position.x < xMax && transform.position.z > zMin && transform.position.z < zMax)
        {           
            gameManager.goToNextScene.gameObject.SetActive(true);
            gameManager.restartButton.gameObject.SetActive(true);
            gameManager.returnToMainMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
