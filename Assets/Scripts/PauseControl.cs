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
    [SerializeField] TimeScript time;

    [SerializeField] GameObject lossUI;
    [SerializeField] GameObject lossBackground;
    private Text lostText;

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

        resultsText = endUI.GetComponent<Text>();
        lostText = lossUI.GetComponent<Text>();
    }

    public bool getGameIsPaused()
    {
        return gameIsPaused;
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
        string kills = GameObject.FindGameObjectWithTag("KillCounter").GetComponent<Text>().text;
        resultsText.text = "Time: " + time.getCurrentTime() + "\nEnemies Killed: " + kills;
        resultsShowing = true;
        endBackground.SetActive(true);
        Time.timeScale = 0f;

    }

    public void loseGame()
    {
        string kills = GameObject.FindGameObjectWithTag("KillCounter").GetComponent<Text>().text;
        lostText.text = "Time: " + time.getCurrentTime() + "\nEnemies Killed: " + kills;
        resultsShowing = true;
        lossBackground.SetActive(true);
        Time.timeScale = 0f;

    }

    void showPauseImage()
    {
        if (getGameIsPaused())
        {
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
