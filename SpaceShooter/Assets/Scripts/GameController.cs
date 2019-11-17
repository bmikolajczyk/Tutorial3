using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    //public GameObject hazard02;
    //public GameObject hazard03;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text winText;
    private int score;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;

    void Start()
    {
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        gameOver = false;
        restart = false;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.Q))
            {
                SceneManager.LoadScene("SampleScene"); // or whatever the name of your scene is
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                //Instantiate(hazard02, spawnPosition, spawnRotation);
                //Instantiate(hazard03, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Q' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You Win! Game created by Brian Mikolajczyk";
            gameOver = true;
            restart = true;
        }
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}