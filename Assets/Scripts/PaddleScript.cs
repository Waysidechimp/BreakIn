using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Located on the paddle gameobject
public class PaddleScript : MonoBehaviour
{
    [SerializeField] PauseControl pause;
    [SerializeField] float speed;
    [SerializeField] float maxX = 10f;

    private float horizontalPosition;

    // Start is called before the first frame update
    void Start()
    {
        
        speed = 10f;
        maxX = 9.2f;
    }

    // Update is called once per frame
    void Update()
    {

        if (!pause.getGameIsPaused())
        {
            horizontalPosition = Input.GetAxis("Horizontal");
            move();
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
            transform.position += Vector3.right* horizontalPosition * speed * Time.deltaTime;
        }
    }


    


}
