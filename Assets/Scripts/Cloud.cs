using UnityEngine;

public class Cloud : MonoBehaviour
{

    private float speed;
    //allows us to reference variables in GameManager script
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //is allowing me to get stuff from GameManager script as well
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //is changing the scale of the clouds
        transform.localScale = transform.localScale * Random.Range(0.1f, 0.6f);
        //is getting the color component of the SpriteRenderer and changing it to some white color at a random transparency (r,g,b, alpha value
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Random.Range(0.1f, 0.7f));
        //is setting the clouds speed to a random value between 3 and 7
        speed = Random.Range(3, 7);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < -gameManager.verticalScreenSize)
        {
            transform.position = new Vector3(Random.Range(-gameManager.horizontalScreenSize, gameManager.horizontalScreenSize), gameManager.verticalScreenSize * 1.2f, 0f);
        }
    }
}
