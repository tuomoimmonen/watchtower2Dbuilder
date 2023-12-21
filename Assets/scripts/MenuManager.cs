using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    AudioSource buttonSound;
    [SerializeField] AudioSource bgMusic;

    Planet planet;

    [SerializeField] GameObject gameOverPanel;
    GameObject gameUI;

    [Header("Score system")]
    private int score = 0;
    private int highScore = 0;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text gameOverScoreText;
    [SerializeField] TMP_Text highScoreText;

    private bool isPaused = false;
    private void Awake()
    {
        //singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
        planet = FindObjectOfType<Planet>();
        //bgMusic.Play();
        Screen.sleepTimeout = SleepTimeout.NeverSleep; //no dimming on mobile
    }

    void Update()
    {
        if(planet == null)
        {
            planet = FindObjectOfType<Planet>();
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            scoreText.gameObject.SetActive(true);
            return;
        }

        if(gameUI == null)
        {
            gameUI = GameObject.FindGameObjectWithTag("GameUI");
        }
        else
        {
            return;
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space)) // pause game
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
        */
    }
    void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "SCORE: " + score.ToString();
        if (score > PlayerPrefs.GetInt("highscore", highScore))
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1.0f;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverPanel.SetActive(false);
        gameUI.SetActive(true);
        score = 0;
        Time.timeScale = 1.0f;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        float randomPitch = Random.Range(0.5f, 1.4f);
        buttonSound.pitch = randomPitch;
        buttonSound.Play();
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", highScore).ToString();
        gameOverScoreText.text = "Score: " + score.ToString();
        gameUI.SetActive(false);
        Time.timeScale = 0f;
    }
}
