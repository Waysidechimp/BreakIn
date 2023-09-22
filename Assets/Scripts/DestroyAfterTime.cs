using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime = 3;
    void Awake()
    {
        Destroy(this.gameObject, destroyTime);
    }
}
