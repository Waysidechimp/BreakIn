using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    [SerializeField] Sprite damagedBrick;
    [SerializeField] Sprite brokenBrick;
    [SerializeField] ParticleSystem brokenParticles;
    [SerializeField] GameObject enemyPrefab;

    SpriteRenderer spriteRenderer;
    [SerializeField] int health = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f), 1);
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
        if (collision.gameObject.CompareTag("Ball"))
        {
            --health;
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f), 1);
            //Debug.Log("Name: " + gameObject.name + ", Health: " + health);
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
                Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
                BrickDie();
            }
        }
    }

    void BrickDie()
    {
        ParticleSystem debris = Instantiate(brokenParticles, this.gameObject.transform);
        debris.transform.SetParent(null);
        debris.transform.position = new Vector3(debris.transform.position.x + 0.5f, debris.transform.position.y - 0.25f, debris.transform.position.z);
        Destroy(this.gameObject);
    }
}
