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

    private bool isMenuUp = true;
    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
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
            timeScript.addTime(5f);
        }
    }
}
