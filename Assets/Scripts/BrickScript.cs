using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickScript : MonoBehaviour
{
    [SerializeField] Sprite damagedBrick;
    [SerializeField] Sprite brokenBrick;
    [SerializeField] ParticleSystem brokenParticles;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] AudioClip breakBreak;

    [SerializeField] GameObject ghostPowerUp;
    [SerializeField] GameObject comboPowerUp;
    [SerializeField] GameObject recallPowerup;

    AudioSource audio;

    private GameObject mainCamera;
    



    private GameObject scoreObject;
    private Text scoreText;
    private TimeScript timeScript;

    private shake Shake;

    //public ComboScript combo;

    private BallScript myBall;

    SpriteRenderer spriteRenderer;
    [SerializeField] int health = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreObject = GameObject.FindGameObjectWithTag("ScoreCounter");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        timeScript = GameObject.FindGameObjectWithTag("Time").GetComponent<TimeScript>();

        myBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>();


        scoreText = scoreObject.GetComponent<Text>();
        Shake = mainCamera.GetComponent<shake>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBrickHealth(int newHealth)
    {
        health = newHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Brick changes sprite based on it's health
        if (collision.gameObject.CompareTag("Ball"))
        {
            health= health-1-myBall.damage;
            updateBrickSprites();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            health -= 1 - myBall.damage;
            updateBrickSprites();
        }
    }

    private void updateBrickSprites()
    {
        if (health == 2 && damagedBrick != null)
        {
            spriteRenderer.sprite = damagedBrick;
        }
        if (health == 1 && brokenBrick != null)
        {
            spriteRenderer.sprite = brokenBrick;
        }
        if (health <= 0)
        {
            //Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);

            AudioSource.PlayClipAtPoint(breakBreak, transform.position);
            BrickDie();
        }
    }

    //Handles everything that should happen upon death of brick
    void BrickDie()
    {
        SpawnPowerup();
        ParticleSystem debris = Instantiate(brokenParticles, this.gameObject.transform);
        debris.transform.SetParent(null);
        debris.transform.position = new Vector3(debris.transform.position.x + 0.5f, debris.transform.position.y - 0.25f, debris.transform.position.z);


        //If its not a door then don't sendmessage
        if (!this.gameObject.CompareTag("Door"))
            transform.parent.SendMessage("ChangeCheckBricks", true);

        Shake.setStart();

        addScore(15);
        timeScript.addTime(5f);

        Destroy(this.gameObject);
    }

    private void addScore(int additionalScore)
    {
        int newScore = int.Parse(scoreText.text) + additionalScore;
        scoreText.text = newScore.ToString();
    }

    void SpawnPowerup()
    {
        if(Random.Range(0, 100) <= 25)
        {
            //0-5
            //Set to 4 because no unique graphic for RecallPowerup
            int powerUpDecision = Random.Range(0, 4);
            switch (powerUpDecision)
            {
                case 1:
                    //Ghost Powerup
                    Instantiate(ghostPowerUp, transform.position, Quaternion.identity);
                    break;
                case 2:
                    //Shotgun shot powerup
                    Instantiate(ghostPowerUp, transform.position, Quaternion.identity);
                    break;
                case 3:
                    //Explosive bullet powerup
                    Instantiate(comboPowerUp, transform.position, Quaternion.identity);
                    break;
                case 4:
                    //Something
                    Instantiate(comboPowerUp, transform.position, Quaternion.identity);
                    break;
                case 5:
                    //Recall Powerup
                    Instantiate(recallPowerup, transform.position, Quaternion.identity);
                    break;

            }
        }
    }

}
