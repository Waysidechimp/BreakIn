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
        // 235
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
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            ballRenderer.color = new Color(ballRenderer.color.r, ballRenderer.color.g, ballRenderer.color.b, 1f);
            unTriggerBricks(bricks);
        }

    }

    void updateBallTag()
    {
        if (isGhostBall)
        {
            ballRenderer.color = new Color(ballRenderer.color.r, ballRenderer.color.g, ballRenderer.color.b , 0.7f);
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            triggerBricks(bricks);
        }
    }

    void triggerBricks(GameObject[] bricks)
    {
        foreach(GameObject g in bricks)
        {
            g.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    void unTriggerBricks (GameObject[] bricks)
    {
        foreach (GameObject g in bricks)
        {
            g.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }


}
