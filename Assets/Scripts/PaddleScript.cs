using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Located on the paddle gameobject
public class PaddleScript : MonoBehaviour
{
    [SerializeField] PauseControl pause;
    [SerializeField] float speed;
    [SerializeField] float maxX = 10f;

    //dashing properties
    private bool canDash;
    private Rigidbody2D rb;
    private bool dashing;
    [SerializeField] float dashingPower;
    [SerializeField] float dashingTime;
    [SerializeField] float dashingCooldown;
    [SerializeField] private TrailRenderer trail;


    private float horizontalPosition;

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
        speed = 10f;
        maxX = 9.2f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!pause.getGameIsPaused())
        {
            if(canDash && Input.GetKey("left shift") && Input.GetButton("Horizontal"))
            {
                horizontalPosition = Input.GetAxis("Horizontal");
                StartCoroutine(Dash());
            }
            else
            {
                horizontalPosition = Input.GetAxis("Horizontal");
                move();
            }
            
        }
        
    }

    /// <summary>
    /// This method moves the ball left and right while also
    /// keeping it in bounds of the screen
    /// </summary>
    private void move()
    {
        if((horizontalPosition>0 && transform.position.x < maxX) || (horizontalPosition< 0 && transform.position.x > -maxX))
        {
            transform.position += Vector3.right * horizontalPosition * speed * Time.deltaTime;
        }
    }



    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;

        Debug.Log("Horizontal Input: " + horizontalPosition);
        Debug.Log("Applied Force: " + new Vector2(dashingPower * horizontalPosition, 0f));


        float originalSpeed = speed;
        speed = dashingPower; // Set a high speed for dashing
        trail.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        speed = originalSpeed; // Restore the original speed
        trail.emitting = false;
        dashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}
