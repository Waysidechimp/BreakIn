using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject brickPrefab;
    [SerializeField] GameObject windowPrefab;
    [SerializeField] GameObject enemyPrefab;

    [Header("Adjustable Castle Values")]
    [SerializeField, Range(1, 10)] int rowNumber = 0;
    [SerializeField, Range(1, 10)] int columnNumber = 0;
    [SerializeField, Range(0.6f, 2)] float rowbuffer = 0;
    [SerializeField, Range(1.7f, 2f)] float columnbuffer = 0;
    [SerializeField] int brickHealth;
    [SerializeField] int windowHealth;

    [Header("Adjustable Enemy Values")]
    [SerializeField, Range(0f, 100f)] float enemySpawnChance;
    [SerializeField] float enemySpawnDelay;
    [SerializeField, Tooltip("Value between 0 and the columnNumber")] int enemyWaveNumber;

    //Values that should not be adjusted by designers
    bool checkBricks = true;
    float timeTilSpawn = 0;
    Vector3 buffer = Vector3.zero;
    Transform startSpawn;
    Transform[,] bricks;
    List<Transform> enemyBricks;
    public bool winFlag = false;

    // Start is called before the first frame update
    void Awake()
    {
        enemyBricks = new List<Transform>();
        bricks = new Transform[columnNumber, rowNumber];
        startSpawn = this.transform;
        SpawnCastle();
    }

    private void Update()
    {
        FindValidSpawnBricks();
        SpawnEnemies();
        CheckWinFlag();
    }

    public bool CheckWinFlag()
    {
        if(enemyBricks.Count == 0)
        {
            winFlag = true;
        }
        return winFlag;
    }

    //Spawns the inital bricks and windows with helper methods. Also spaces these gameobjects.
    void SpawnCastle()
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
                else if (i == 4 && j == 2)
                {
                    SpawnWindow(i, j);
                }
                else
                {
                    SpawnBrick(i, j);
                }
            }
            buffer.x = 0;
            buffer.y -= rowbuffer;
        }
    }
    //Spawns the bricks. Adds them to the bricks array
    private void SpawnBrick(int i, int j)
    {
        GameObject brick = Instantiate(brickPrefab, startSpawn.position + buffer, Quaternion.identity);
        bricks[j,i] = brick.transform;
        brick.name = "brick: R: " + i + " C: " + j;
        brick.transform.parent = this.transform;
        brick.GetComponent<BrickScript>().SetBrickHealth(brickHealth);
        buffer.x += columnbuffer;
    }
    //Spawns the windows. Adds them to the bricks array
    private void SpawnWindow(int i, int j)
    {
        GameObject window = Instantiate(windowPrefab, startSpawn.position + buffer, Quaternion.identity);
        bricks[j, i] = window.transform;
        window.name = "window: R: " + i + " C: " + j;
        window.transform.parent = this.transform;
        window.GetComponent<BrickScript>().SetBrickHealth(windowHealth);
        buffer.x += columnbuffer;
    }
    //Spawns the enemies. Utilizes the enemyBricks List to decide where to spawn them.
    void SpawnEnemies()
    {
        timeTilSpawn += Time.deltaTime;
        if (timeTilSpawn >= enemySpawnDelay)
        {
            timeTilSpawn = 0;

            Debug.Log("Starting");
            int enemiesSpawned = 0;
            foreach (Transform t in enemyBricks)
            {
                if (UnityEngine.Random.Range(0, 101) <= enemySpawnChance)
                {
                    Debug.Log("Spwning Enemy");
                    GameObject enemy = Instantiate(enemyPrefab, t.position, quaternion.identity);
                    enemy.transform.parent = this.transform;
                    enemiesSpawned++;
                }
                if (enemiesSpawned >= enemyWaveNumber)
                {
                    break;
                }
            }
            Debug.Log("Ending");
            enemiesSpawned = 0;
        }

    }
    //Finds bricks that should be in the enemyBricks List.
    private void FindValidSpawnBricks()
    {
        if (checkBricks)
        {
            enemyBricks.Clear();
            for (int j = 0; j < columnNumber; j++)
            {
                Transform validBrick = null;

                //Check bottom row for valid bricks. If row has invalid brick, move up.
                for (int i = rowNumber - 1; i >= 0; i--)
                {
                    if (bricks[j, i] != null)
                    {
                        validBrick = bricks[j, i];
                        break;
                    }
                }
                //add validBrick to enemyBricks List.
                if (validBrick != null)
                {
                    enemyBricks.Add(validBrick);
                }
            }
            checkBricks = false;
        }
    }
    //Allows other scripts to message this method to recheck the valid bricks.
    public void ChangeCheckBricks(bool check)
    {
        checkBricks = check;
    }
}
