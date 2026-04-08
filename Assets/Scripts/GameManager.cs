using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cloudPrefab;
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;

        createSky();
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 5, 5);
        AddScore(score);
    }

    void createSky()
    {
        for(int i = 0; i < 30; i++)
        {
            //Instantiate(thing you spawn, vector position, rotation)
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize * .9f, horizontalScreenSize * .9f), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize * .9f, horizontalScreenSize* .9f), verticalScreenSize, 0), Quaternion.identity);
    }
    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize * .9f, horizontalScreenSize * .9f), verticalScreenSize, 0), Quaternion.identity);
    }

    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        scoreText.text = "Score: " + score;
    }

    //function is setting the text to how many lives there are using the parameter
    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "lives " + currentLives;
    }
}