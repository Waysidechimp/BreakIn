using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Located on PlayerBase game object
public class PauseControl : MonoBehaviour
{

    private bool gameIsPaused = true;

    [SerializeField] GameObject pauseImage;
    [SerializeField] TimeScript timeScript;
    [SerializeField] GameObject billboard;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject endUI;
    [SerializeField] GameObject endBackground;

    [SerializeField] GameObject lossUI;
    [SerializeField] GameObject lossBackground;
    [SerializeField] GameObject ScoreAmount;
    [SerializeField] shake shake;
    private int deathToll = 0;
    private Text lostText;
    private Text scoreText;
    private Text resultsText;
    private BrickSpawner leftBrickSpawn;
    private BrickSpawner rightBrickSpawn;
    private bool resultsShowing;


    private bool isMenuUp = true;
    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
        resultsShowing = false;
        leftBrickSpawn = leftWall.GetComponent<BrickSpawner>();
        rightBrickSpawn = rightWall.GetComponent<BrickSpawner>();

        scoreText = ScoreAmount.GetComponent<Text>();
        resultsText = endUI.GetComponent<Text>();
        lostText = lossUI.GetComponent<Text>();
    }

    public bool getGameIsPaused()
    {
        return gameIsPaused;
    }

    public void addDeathToll()
    {
        deathToll++;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMenuUp)
        {
//            Debug.Log("Pause: " + gameIsPaused);
            gameIsPaused = !gameIsPaused;
            PauseGame();
            showPauseImage();
        }

        if (rightBrickSpawn.winFlag && leftBrickSpawn.winFlag && !resultsShowing)
            winGame();


    }

    void winGame()
    {
        resultsText.text = resultsTextFormatter();
        resultsShowing = true;
        endBackground.SetActive(true);
        shake.start = false;
        Time.timeScale = 0f;

    }

    public void loseGame()
    {
        lostText.text = resultsTextFormatter();
        resultsShowing = true;
        lossBackground.SetActive(true);
        shake.start = false;
        Time.timeScale = 0f;

    }

    private string resultsTextFormatter()
    {
        string result;
        result = "Score: " + scoreText.text +
     "\nTime: " + timeScript.getCurrentTime() +
     "\nScore Multiplyer: " + timeScript.getTime() +
     "\nFinal Score: " + formatScore() + "\nEnemies Killed: " + deathToll;

        return result;
    }

    private int formatScore()
    {
        int result;
        if(timeScript.getTime() <= 0)
        {
            result = (int)(float.Parse(scoreText.text) * 1);
        }
        else
        {
            result = (int)(float.Parse(scoreText.text) * timeScript.getTime());
        }
        return result;
    }

    void showPauseImage()
    {
        if (getGameIsPaused())
        {
            Debug.Log(formatScore());
            pauseImage.SetActive(true);
        }
        else
        {
            pauseImage.SetActive(false);
        }
    }

    public void startGame()
    {
        isMenuUp = false;
        gameIsPaused = false;
        Debug.Log("Started game: " + isMenuUp);
        PauseGame();
    }

    void PauseGame()
    {
        if (gameIsPaused) 
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            timeScript.loseTime(5f);

            GameObject bill = Instantiate(billboard, collision.gameObject.transform.position, Quaternion.identity);
            bill.transform.position = new Vector3(bill.transform.position.x + 0.5f, bill.transform.position.y - 0.25f, bill.transform.position.z);
        }
    }
}
