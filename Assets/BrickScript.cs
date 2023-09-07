using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    int health = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f), 1);
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
            gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f), 1);
            //Debug.Log("Name: " + gameObject.name + ", Health: " + health);
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
