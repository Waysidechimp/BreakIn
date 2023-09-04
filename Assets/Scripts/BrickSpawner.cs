using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab;
    [SerializeField] Transform startSpawn;
    [SerializeField, Range(1, 5)] int rowNumber = 0;
    [SerializeField, Range(1, 10)] int columnNumber = 0;
    [SerializeField, Range(0.6f, 2)] float rowbuffer = 0;
    [SerializeField, Range(1.7f, 2f)] float columnbuffer = 0;

    Vector3 buffer = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < rowNumber; i++)
        {
            for (int j=0; j < columnNumber; j++)
            {
                GameObject brick = Instantiate(brickPrefab, startSpawn.position + buffer, Quaternion.identity);
                brick.transform.parent = this.transform;
                buffer.x += columnbuffer;
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
