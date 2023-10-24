using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerComboScript : MonoBehaviour
{
    GameObject ball;
    [SerializeField] float fallSpeed;
    [SerializeField] AudioClip pickupSound;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");

    }

    private void Update()
    {
        gameObject.transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle") || collision.CompareTag("Ball"))
        {
            ball.GetComponent<BallScript>().combo = 11;
            ball.GetComponent<BallScript>().damage = 2;
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(this.gameObject);
        }
    }
}
