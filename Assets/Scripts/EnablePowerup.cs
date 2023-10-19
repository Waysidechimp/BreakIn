using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePowerup : MonoBehaviour
{
    GameObject ball;
    GhostBallin ghost;
    [SerializeField] float fallSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ghost = ball.GetComponent<GhostBallin>();
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector2.down*Time.deltaTime*fallSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Paddle") || collision.CompareTag("Ball"))
        {
            ghost.isGhostBall = true;
            Destroy(this.gameObject);
        }
    }
}
