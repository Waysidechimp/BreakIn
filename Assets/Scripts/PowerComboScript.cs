using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerComboScript : MonoBehaviour
{
    GameObject ball;
    BallScript ballScript;
    [SerializeField] float fallSpeed;
    [SerializeField] AudioClip pickupSound;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballScript = ball.GetComponent<BallScript>();
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle") || collision.CompareTag("Ball"))
        {
            ballScript.powerUpUpdate(gameObject.tag);
            Destroy(this.gameObject);
        }
    }
}
