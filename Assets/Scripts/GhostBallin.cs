using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBallin : MonoBehaviour
{
    bool isGhostBall = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            isGhostBall = !isGhostBall;
            updateBallTag();
        }


    }

    void updateBallTag()
    {
        if (isGhostBall)
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            triggerBricks(bricks);
        }
        else
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            unTriggerBricks(bricks);
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
