using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPool; // лист для добавления различных типов врагов
    [SerializeField] private float radius = 10f;
    private Transform planet;

    private void Awake()
    {
        planet = FindObjectOfType<Planet>().gameObject.transform;
    }

    private void Update()
    {
        if(LevelProgress.levelTime % 2 <= 0.05 && LevelProgress.pause == false)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var enemy = PoolManager.GetObject(enemyPool[Random.Range(0, enemyPool.Count)].gameObject.name, transform.position, Quaternion.identity);
        Vector3 random = new Vector3(Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad), Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad), 0);
        Vector3 randomPosition = planet.position + random;
        var dir = (planet.position - randomPosition).normalized;
        enemy.transform.position = planet.position + (dir * radius);
    }
}
