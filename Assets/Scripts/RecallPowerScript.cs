using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallPowerScript : MonoBehaviour
{


    GameObject ball;
    [SerializeField] float fallSpeed;

    [SerializeField] AudioClip pickupSound;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle") || collision.CompareTag("Ball"))
        {
            ball.GetComponent<BallScript>().updateRecall(1);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(this.gameObject);
        }
    }

}
