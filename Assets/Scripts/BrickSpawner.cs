using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab;
    [SerializeField] GameObject windowPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField, Range(1, 10)] int rowNumber = 0;
    [SerializeField, Range(1, 10)] int columnNumber = 0;
    [SerializeField, Range(0.6f, 2)] float rowbuffer = 0;
    [SerializeField, Range(1.7f, 2f)] float columnbuffer = 0;
    [SerializeField] int brickHealth;
    [SerializeField] int windowHealth;

    Vector3 buffer = Vector3.zero;
    Transform startSpawn;
    Transform[,] bricks; 
    // Start is called before the first frame update
    void Start()
    {
        bricks = new Transform[columnNumber, rowNumber];
        startSpawn = this.transform;
        SpawnBrick();
    }

    void SpawnBrick()
    {
        for (int i = 0; i < rowNumber; i++)
        {
            for (int j = 0; j < columnNumber; j++)
            {
                if (i == 0 && j % 2 != 0)
                {
                    bricks[j, i] = null;
                    buffer.x += columnbuffer;
                }
                /*else if (i == 3 && j == 2)
                {
                    bricks[j, i] = null;
                    buffer.x += columnbuffer;
                }*/
                else if (i == 4 && j == 2)
                {
                    GameObject window = Instantiate(windowPrefab, startSpawn.position + buffer, Quaternion.identity);
                    bricks[j, i] = window.transform;
                    window.name = "window: R: " + i + " C: " + j;
                    window.transform.parent = this.transform;
                    window.GetComponent<BrickScript>().SetBrickHealth(windowHealth);
                    buffer.x += columnbuffer;
                }
                else
                {
                    GameObject brick = Instantiate(brickPrefab, startSpawn.position + buffer, Quaternion.identity);
                    bricks[j, i] = brick.transform;
                    brick.name = "brick: R: " + i + " C: " + j;
                    brick.transform.parent = this.transform;
                    brick.GetComponent<BrickScript>().SetBrickHealth(brickHealth);
                    buffer.x += columnbuffer;
                }
            }
            buffer.x = 0;
            buffer.y -= rowbuffer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
