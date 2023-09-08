using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxX = 10f;

    private float horizontalPosition;

    // Start is called before the first frame update
    void Start()
    {
        speed = 7;
        maxX = 9f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalPosition = Input.GetAxis("Horizontal");
        move(); 
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