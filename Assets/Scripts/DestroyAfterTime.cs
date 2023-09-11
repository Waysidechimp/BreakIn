using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public int destroyTime = 3;
    void Awake()
    {
        Destroy(this.gameObject, destroyTime);
    }
}
