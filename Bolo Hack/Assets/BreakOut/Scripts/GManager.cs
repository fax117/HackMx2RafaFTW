using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Using UI elements
using UnityEngine.SceneManagement; //To load and reload scenes and levels

public class GManager : MonoBehaviour
{

    public int lives = 3;
    public int puffles = 24;
    public int scoreValue = 0;

    public float resetDelay = 1f;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image deadScreen;
    public Text scoreText;
    public GameObject youDiedDS;
    public GameObject deathParticles;
    public GameObject pallet;
    public GameObject victoryText;
    public GameObject defeatText;
    public GameObject buttonManager;

    public AudioClip deathSFX;
    public AudioClip puffleDeathSFX;
    public AudioClip greatScoreEventSFX;
    public AudioClip victorySFX;
    public AudioClip youDiedSFX;
    private AudioSource audioSource;

    public Button resetButton;

    public bool gameStatus = true;

    private GameObject clonePallet;

    public static GManager instance = null; // Static allows it to be accessed from other objects in the game. By having only 1 instance, control over the Game Manager's variables becomes versatile.

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject); //So only one Game Manager can exist at a time.
        }
        resetButton.onClick.AddListener(ReloadLevel);
        audioSource = GetComponent<AudioSource>();
        Setup();
    }

    public void Setup()
    {
        clonePallet = Instantiate(pallet, transform.position, Quaternion.identity) as GameObject;
    }

    void CheckGameOver()
    {
        if(puffles < 1)
        {
            //Player Wins
            audioSource.PlayOneShot(victorySFX);
            youDiedDS.gameObject.SetActive(true);
            deadScreen.enabled = false;
            victoryText.SetActive(true);
            buttonManager.SetActive(true);
            Time.timeScale = .25f;
            gameStatus = false;
        }
        if(lives < 1)
        {
            //defeatText.SetActive(true); Esto ya no porque incluimos imagen parodia de Dark Souls
            youDiedDS.gameObject.SetActive(true);
            audioSource.PlayOneShot(youDiedSFX, 5.0f);
            buttonManager.SetActive(true);
            Time.timeScale = .25f;
            gameStatus = false;
        }
    }

    void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void LoseLife()
    {
        if (gameStatus)
        {
            audioSource.PlayOneShot(deathSFX);
            lives--;
            if (lives == 2)
            {
                heart3.enabled = false;
            }
            if (lives == 1)
            {
                heart2.enabled = false;
            }
            if (lives == 0)
            {
                heart1.enabled = false;
            }
            //Instantiate(deathParticles, ball.transform.position, Quaternion.identity); Needs checking
            if (lives > 0)
            {
                Invoke("PartialReset", 1.5f);
            }
            CheckGameOver();
        }
    }

    public void DestroyPuffle(int puffleType)
    {
        if(puffleType < 400)
        {
            audioSource.PlayOneShot(puffleDeathSFX);
        }
        else if(puffleType >= 400)
        {
            audioSource.PlayOneShot(greatScoreEventSFX);
        }
        puffles--;
        Debug.Log("Puffles in the level left: " + puffles);
        CheckGameOver();
    }

    public void PartialReset()
    {
        Destroy(clonePallet);
        Setup();
    }

    public void UpdateScore(int puffleType)
    {
        Debug.Log("The puffle type value is: " + puffleType);
        scoreValue = scoreValue + puffleType;
        Debug.Log("Current Score Value value is: " + scoreValue);
        if (scoreValue < 100)
        {
            scoreText.text = "000" + scoreValue.ToString();
        }
        else if (scoreValue < 1000)
        {
            scoreText.text = "00" + scoreValue.ToString();
        }
        else if (scoreValue < 10000)
        {
            scoreText.text = "0" + scoreValue.ToString();
        }
        else if (scoreValue < 100000)
        {
            scoreText.text = scoreValue.ToString();
        }
    }

    public bool GameStatus()
    {
        return gameStatus;
    }

    public void ReloadLevel()
    {
        Reset();
        //Invoke("Reset", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
