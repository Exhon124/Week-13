using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemyOne;
    public GameObject enemy2;
    public GameObject cloud;
    public GameObject powerup;
    public GameObject coin;


    public AudioClip powerUp;
    public AudioClip powerDown;

    public int cloudSpeed;

    private bool isPlayerAlive;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI livesText;
    private Player playerScript;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerInstance = Instantiate(player, transform.position, Quaternion.identity);
        playerScript = playerInstance.GetComponent<Player>();

        InvokeRepeating("CreateEnemyOne", 1f, 3f);
        InvokeRepeating("CreateEnemy2", 1f, 4f);
        InvokeRepeating("CreateCoin", 1f, 4f);
        StartCoroutine(CreatePowerup());
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        isPlayerAlive = true;
        cloudSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + playerScript.lives;

        Restart();   
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }

    void CreateEnemy2()
    {
        // Randomly choose a negative positive or neither number
        int side = Random.Range(-1, 2);
        // Use the random number to determine if the entity will start on the left or right
        if (side == -1)
        {
            Instantiate(enemy2, new Vector3(-11, Random.Range(1f, 4f), 0), Quaternion.identity);
        }
        else if (side == 1)
        {
            Instantiate(enemy2, new Vector3(11, Random.Range(1f, 4f), 0), Quaternion.identity);
        }
    }

    IEnumerator CreatePowerup()
    {
        Instantiate(powerup, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(3f, 6f));
        StartCoroutine(CreatePowerup());
    }

    void CreateCoin()
    {
        // Randomly choose a negative positive or neither number
        int side = Random.Range(-1, 2);
        // Use the random number to determine if the entity will start on the left or right
        if (side == -1)
        {
            Instantiate(coin, new Vector3(-11, Random.Range(-0.1f, -4f), 0), Quaternion.identity);
        }
        else if (side == 1)
        {
            Instantiate(coin, new Vector3(11, Random.Range(-0.1f, -4f), 0), Quaternion.identity);
        }
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }

    public void EarnScore(int newScore)
    {
        score = score + newScore;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isPlayerAlive = false;
        CancelInvoke();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        cloudSpeed = 0;
    }

    void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void UpdatePowerupText(string whichPowerup)
    {
        powerupText.text = whichPowerup;
    }

    public void PlayPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUp, Camera.main.transform.position);
    }

    public void PlayPowerDown()
    {
        AudioSource.PlayClipAtPoint(powerDown, Camera.main.transform.position);
    }
}
