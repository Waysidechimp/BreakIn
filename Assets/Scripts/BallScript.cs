using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Located on the ball
public class BallScript : MonoBehaviour
{
    [SerializeField] AudioClip ballToWall;
    [SerializeField] AudioClip ballToBrick;
    [SerializeField] AudioClip ballToEnemy;
    AudioSource audio;

    //If with paddle is true follow the paddle and shoot forward when
    //player clicks
    [SerializeField] PauseControl pause; 
    [SerializeField] bool withPaddle = true;
    [SerializeField] GameObject paddle;
    private Rigidbody2D rb;
    [SerializeField]
    float speedInUnitPerSecond;
    //can remove serializefield
    [SerializeField]
    int wallBounce;
    [SerializeField]
    int bounceLimit = 4;



    //temp
    [SerializeField]
    float upndown;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        wallBounce = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (withPaddle)
            followPaddle();


        if(!pause.getGameIsPaused() && withPaddle)
        fireBall();

        

        
        
        upndown = rb.velocity.y;
        speedInUnitPerSecond = rb.velocity.magnitude;

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
                //rb.velocity = (Vector2.left ) * 7f;
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
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided; " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Wall"))
        {
            audio.clip = ballToWall;
            audio.Play();
        }
        if (collision.gameObject.CompareTag("Brick"))
        {
            audio.clip = ballToBrick;
            audio.Play();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audio.clip = ballToEnemy;
            audio.Play();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            audio.clip = ballToWall;
            audio.Play();
            wallBounce++;
            if (wallBounce >= bounceLimit)
            {
                if (rb.velocity.y < 1 && rb.velocity.y > -1) {
                    if (rb.velocity.y >= 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 1f);
                    }
                    else {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 1f);
                    }
                }
                
            }
        }
        if (collision.gameObject.tag != "Wall")
        {
            
            wallBounce = 0;
        }
    }
}
