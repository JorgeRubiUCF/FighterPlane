using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider2 : MonoBehaviour
{

    public bool goingUp;
    public bool direction;
    public float speed;
    public float randomizer;
    //NECESSARY TO ACCESS GAME MANAGER VARIABLES
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        randomizer = Random.Range(1f, 4f);
        if (randomizer >= 2.0f)
        {
            direction = false;
        }
        else
        {
            direction = true;
        }
        speed = 2f;
        //THIS IS NECESSARY TOO
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(goingUp)
        {
            if (transform.position.x > gameManager.horizontalScreenSize || transform.position.x < -gameManager.horizontalScreenSize)
            {
                direction = !direction;
            }
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (direction)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed * 1.2f);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 1.2f);
            }


        }
        else if(goingUp == false)
        {
            if (transform.position.x >= gameManager.horizontalScreenSize || transform.position.x <= -gameManager.horizontalScreenSize)
            {
                direction = !direction;
            }
            if (direction)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed * 1.2f);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 1.2f);
            }
                        transform.Translate(Vector3.down * Time.deltaTime * speed);

        }

        if (transform.position.y < -gameManager.verticalScreenSize * 1.25f || transform.position.y > gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }
    }


}