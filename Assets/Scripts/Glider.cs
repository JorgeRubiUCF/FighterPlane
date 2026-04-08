using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{

    public bool goingUp;
    public float speed;
    //NECESSARY TO ACCESS GAME MANAGER VARIABLES
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        //THIS IS NECESSARY TOO
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if(goingUp == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        if (transform.position.y < -gameManager.verticalScreenSize * 1.25f || transform.position.y > gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }
    }


}