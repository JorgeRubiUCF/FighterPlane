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

    void Start()
    {
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
        lives--;
        gameManager.ChangeLivesText(lives);
        if(lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
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