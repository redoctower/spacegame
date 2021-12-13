using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private PoolObject _poolObject;

    private void Awake()
    {
        _poolObject = GetComponent<PoolObject>();
    }

    private void Update()
    {
        if (LevelProgress.pause)
        {
            BackToPool();
        }
    }

    public void Die()
    {
        GetComponent<PoolObject>().ReturnToPool();
        LevelProgress.levelTime += 3;
    }
    private void BackToPool()
    {
        _poolObject.ReturnToPool();
    }
}
