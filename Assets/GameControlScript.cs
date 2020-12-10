using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;
    public Text levelText;
    int score = 0;
    int level = 0;
    int lives = 0;
    int hiscore;
    private GameObject[] lifeIndicators;

    // Pattern to make this object live forever (singleton)
    private static bool created = false;

    void Awake()
    {
        if(!created)
        {
            DontDestroyOnLoad(this.gameObject);
            hiscore = PlayerPrefs.GetInt("HiScore");
            created = true;
            lifeIndicators = GameObject.FindGameObjectsWithTag("Lives");
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        updateScoreBoard();
        updateLifeIndicator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int levelUp()
    {
        level++;
        levelText.text = "LEVEL: " + level.ToString();
        return level;
    }

    public int lifeLost()
    {
        lives--;
        updateLifeIndicator();
        return lives;
    }

    public void updateScore(int valueToAdd)
    {
        score += valueToAdd;
        if (score > hiscore) {
            hiscore = score;
        }
        updateScoreBoard();
    }

    void updateScoreBoard() {
        scoreText.text = "SCORE: " + score.ToString("D6");
        hiScoreText.text = "HISCORE: " + hiscore.ToString("D6");
    }

    void updateLifeIndicator()
    {
        int i = 0;
        foreach (GameObject obj in lifeIndicators)
        {
            i++;
            if (i > lives)
            {
                obj.SetActive(false);
            }
        }
    }

    public void resetLifeIndicator()
    {
        lives = 3;
        foreach (GameObject obj in lifeIndicators)
        {
            obj.SetActive(true);
        }
    }

    public void resetScore()
    {
        score = 0;
        level = 0;
        updateScoreBoard();
    }

    public void endGame()
    {
        PlayerPrefs.SetInt("HiScore", hiscore);
        SceneManager.LoadScene("GameOverScene");
        StartCoroutine(ExampleCoroutine(10));
    }

    IEnumerator ExampleCoroutine(int seconds)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
