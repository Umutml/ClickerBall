using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = true;
    public List<GameObject> targets;
    public GameObject titleScreen;
    private int score;
    private float spawnRate = 1;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoretext;
    public GameObject restartButton;
    public TextMeshProUGUI livesText;
    public GameObject PausedPanel;
    private int lives;
    private bool timeStop = false;

    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !timeStop)
        {
            timeStop = true;
            Time.timeScale = 0;
            PausedPanel.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && timeStop)
        {
            Time.timeScale = 1;
            timeStop = false;
            PausedPanel.SetActive(false);
        }
    }

    IEnumerator SpawnTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
        
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE = " + score;
    }

    public void GameOver()
    {
        finalScoretext.text = "FINAL SCORE = " + score;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        finalScoretext.gameObject.SetActive(true);
        isGameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameOver = false;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        UpdateLives(3);
        titleScreen.SetActive(false);
        
    }
    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "LIVES = " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

}
