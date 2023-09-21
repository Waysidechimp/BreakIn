using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Located on the ball
public class BallScript : MonoBehaviour
{
    //If with paddle is true follow the paddle and shoot forward when
    //player clicks
    [SerializeField] PauseControl pause; 
    [SerializeField] bool withPaddle = true;
    [SerializeField] GameObject paddle;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (withPaddle)
            followPaddle();


        if(!pause.getGameIsPaused() && withPaddle)
        fireBall();
    }

    void followPaddle()
    {
        Vector3 temp = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 1, paddle.transform.position.z);
        transform.position = temp;
    }

    void fireBall()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("space")){

            if (Input.GetAxis("Horizontal") < -0.2) //left
            {
                rb.velocity = (Vector2.left + Vector2.up) * 7f;
            }
            else if (Input.GetAxis("Horizontal") > 0.2) //right
            {
                rb.velocity = (Vector2.right + Vector2.up) * 7f;
            }
            else //not moving go straight up
            {
                rb.velocity = Vector2.up * 10f;
            }

            //Always after the paddle is shot change it to not with paddle
            withPaddle = false;
        }
    }



    /// <summary>
    /// If comes into contact with playerBase
    /// reset volcity and relaunch the ball.
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBase")
        {
            rb.velocity = Vector3.zero;
            withPaddle = true;
        }
    }
}
