using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //how to define a variable
    //1. access modifier: public or private
    //2. data type: int, float, bool, string
    //3. variable name: camelCase
    //4. value: optional

    public int lives;
    //references the GameManager script;
    public GameManager gameManager;
    public GameObject explosionPrefab;
    private float playerSpeed;

    private float horizontalInput;
    private float verticalInput;


    public GameObject bulletPrefab;

    public GameObject thruster;
    public GameObject shield;
    public int weaponType;
    public bool shieldActive;

    void Start()
    {
        shieldActive = false;
        weaponType = 1;
        playerSpeed = 6f;
        lives = 3;
        //this is setting a reference to the game manager, allowing us to use it
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);

        //This function is called at the start of the game

    }

    void Update()
    {
        //This function is called every frame; 60 frames/second
        Movement();
        Shooting();

    }

    public void LoseALife()
    {
        if (!shieldActive)
        {
            lives--;
        }
        if (shieldActive)
        {
            shield.SetActive(false);
            shieldActive = false;

        }
        gameManager.ChangeLivesText(lives);
        if(lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.GameOver();
            Destroy(this.gameObject);
        }
    }

    //this is basically cookie cutter for all the other powerups

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        shield.SetActive(false);
        shieldActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerUpText(5);
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        playerSpeed = 5f;
        thruster.SetActive(false);
        gameManager.PlaySound(2);
        gameManager.ManagePowerUpText(5);
    }

    IEnumerator WeaponsPowerDown()
    {
        yield return new WaitForSeconds(5);
        weaponType = 1;
        gameManager.PlaySound(2);
        gameManager.ManagePowerUpText(5);
    }



    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "PowerUp")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerUp = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch(whichPowerUp)
            {
                case 1:
                    playerSpeed = 10f;
                    //StartCoroutine
                    //thruster active
                    thruster.SetActive(true);
                    gameManager.ManagePowerUpText(1);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    //set weapon type 2
                    weaponType = 2;
                    //weapon powerdown coroutine
                    gameManager.ManagePowerUpText(2);
                    StartCoroutine(WeaponsPowerDown());
                    break;
                case 3:
                    //set weapon type 3
                    weaponType = 3;
                    //weapon powerdown coroutine
                    gameManager.ManagePowerUpText(3);
                    StartCoroutine(WeaponsPowerDown());
                    break;
                case 4:
                    //set shield active - turn on a bool and game object
                    //set shield power down coroutine
                    shield.SetActive(true);
                    shieldActive = true;
                    gameManager.ManagePowerUpText(4);
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
        }
    }

    void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //put a switch case here that uses the different weapon type to fire different amounts of bullets
            //IMPORTANT
            switch (weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
        }
    }

    void Movement()
    {
        //Read the input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        //is using now the horizontal and vertical screen size variables form the GameManager script, rather than having variables for just here
        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        //Player leaves the screen horizontally
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        //tries to keep the player in the bottom half of the screen
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        if (transform.position.y < -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenSize, 0);
        }
        //Player leaves the screen vertically
        //if (transform.position.y > verticalScreenSize || transform.position.y <= -verticalScreenSize)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        //}
    }

}