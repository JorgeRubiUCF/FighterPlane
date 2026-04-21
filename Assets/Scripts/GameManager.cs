using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cloudPrefab;
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject gameOverMenu;
    public GameObject powerUpPrefab;
    public GameObject lifePowerUpPrefab;
    public GameObject coinPrefab;
    public GameObject audioPlayer;

    public AudioClip coinPickup;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;
    private bool gameOver;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        gameOver = false;

        createSky();
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 5, 5);
        AddScore(score);
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnCoin());
        powerUpText.text = "No Power ups yet!";
    }

    public void ManagePowerUpText(int powerUpType)
    {
        switch(powerUpType)
        {
            case 1:
                powerUpText.text = "Speed!";
                break;
            case 2:
                powerUpText.text = "Double Weapons!";
                break;
            case 3:
                powerUpText.text = "Triple Weapons!";
                break;
            case 4:
                powerUpText.text = "Shield!";
                break;
            case 5:
                powerUpText.text = "Life up!";
                break;
            case 6:
                powerUpText.text = "Max Lives! +1 Score!";
                break;
            default:
                powerUpText.text = "No power ups yet!";
                break;
        }
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
            case 3:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(coinPickup);
                break;
        }
    }

    IEnumerator SpawnCoin()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreateCoin();
        StartCoroutine(SpawnCoin());
    }

    IEnumerator SpawnPowerUp()
    {
        int lifeSpawnChance = Random.Range(1, 11);
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        if (lifeSpawnChance == 1)
        {
            CreateLifePowerUp();
        }
        else
        {
            CreatePowerUp();
        }
        StartCoroutine(SpawnPowerUp());
    }

    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize) * 0.8f, Random.Range(-verticalScreenSize * 0.8f, -0.8f), 0), Quaternion.identity);
    }

    void CreatePowerUp()
    {
        Instantiate(powerUpPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize) * 0.8f, Random.Range(-verticalScreenSize * 0.8f, -0.8f), 0), Quaternion.identity);
    }
    void CreateLifePowerUp()
    {
        Instantiate(lifePowerUpPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize) * 0.8f, Random.Range(-verticalScreenSize * 0.8f, -0.8f), 0), Quaternion.identity);
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
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        //set our game over object to active
        gameOverMenu.SetActive(true);
        gameOver = true;

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
        livesText.text = "lives: " + currentLives;
    }
}