using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    [SerializeField] Sprite damagedBrick;
    [SerializeField] Sprite brokenBrick;
    [SerializeField] ParticleSystem brokenParticles;
    [SerializeField] GameObject enemyPrefab;

<<<<<<< HEAD
    

=======

>>>>>>> parent of b647893 (Added Audio)


    private GameObject mainCamera;

<<<<<<< HEAD
    private GameObject scoreObject;
    private Text scoreText;
    private TimeScript timeScript;
=======
>>>>>>> parent of b647893 (Added Audio)

    private shake Shake;

    SpriteRenderer spriteRenderer;
    [SerializeField] int health = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
            --health;
            if(health == 2 && damagedBrick != null)
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
                BrickDie();
            }
        }
    }

    //Handles everything that should happen upon death of brick
    void BrickDie()
    {
        ParticleSystem debris = Instantiate(brokenParticles, this.gameObject.transform);
        debris.transform.SetParent(null);
        debris.transform.position = new Vector3(debris.transform.position.x + 0.5f, debris.transform.position.y - 0.25f, debris.transform.position.z);

        //If its not a door then don't sendmessage
        if(!this.gameObject.CompareTag("Door"))
        transform.parent.SendMessage("ChangeCheckBricks", true);

        Shake.setStart();

        Destroy(this.gameObject);
    }

}
