using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBallin : MonoBehaviour
{
    public bool isGhostBall = false;
    [SerializeField] GameObject ballSprite;
    SpriteRenderer ballRenderer;
    // Start is called before the first frame update
    void Start()
    {
        ballSprite = gameObject.transform.GetChild(0).gameObject;
        ballRenderer = ballSprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        updateBallTag();

        if (gameObject.transform.position.y <= -3.3 && isGhostBall)
        {
            isGhostBall = false;
            ballRenderer.color = new Color(ballRenderer.color.r, ballRenderer.color.g, ballRenderer.color.b, 1f);

            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            GameObject[] windows = GameObject.FindGameObjectsWithTag("Window");
            unTriggerBricks(bricks, windows);
        }

    }

    void updateBallTag()
    {
        if (isGhostBall)
        {
            Debug.Log("I'm ghost ballin");
            ballRenderer.color = new Color(ballRenderer.color.r, ballRenderer.color.g, ballRenderer.color.b , 0.7f);

            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            GameObject[] door = GameObject.FindGameObjectsWithTag("Door");
            GameObject[] windows = GameObject.FindGameObjectsWithTag("Window");

            triggerBricks(bricks, windows);
        }
    }

    void triggerBricks(GameObject[] bricks, GameObject[] window)
    {
        foreach(GameObject g in bricks)
        {
            g.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        foreach (GameObject w in window)
        {
            w.GetComponent<BoxCollider2D>().isTrigger = true;

        }

    }

    void unTriggerBricks (GameObject[] bricks, GameObject[] window)
    {
        foreach (GameObject g in bricks)
        {
            g.GetComponent<PolygonCollider2D>().isTrigger = false;
        }

        foreach(GameObject w in window)
        {
            w.GetComponent<BoxCollider2D>().isTrigger = false;

        }

    }


}
