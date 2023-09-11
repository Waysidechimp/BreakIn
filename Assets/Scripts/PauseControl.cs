using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{

    private bool gameIsPaused = true;

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
        if (!isMenuUp && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause: " + gameIsPaused);
            gameIsPaused = !gameIsPaused;
            PauseGame();
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
