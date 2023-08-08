using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpawner : MonoBehaviour
{
    public float initialTimeBetweenSpawns = 2f;
    public float maxTimeBetweenSpawns = 5f;
    private float timeBetweenSpawns;
    private float nextSpawnTime;

    private GameObject SpawnEffect;

    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] spawnables;

    public Transform[] spawnPoints;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private float gameTimer = 0f;
    public float timeToIncreaseSpawn = 30f;
    private float spawnIncreaseInterval = 5f;

    private void Start()
    {
        timeBetweenSpawns = initialTimeBetweenSpawns;
        nextSpawnTime = Time.time + timeBetweenSpawns;
    }

    private void OnEnable()
    {
        nextSpawnTime = Time.time + timeBetweenSpawns;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;

        if (gameTimer >= timeToIncreaseSpawn)
        {
            timeToIncreaseSpawn += spawnIncreaseInterval;
            IncreaseSpawnRate();
        }

        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + timeBetweenSpawns;
            Spawn();
        }
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var spawnable in spawnables)
        {
            if (spawnChance < spawnable.spawnChance)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject spawnedObject = Instantiate(spawnable.prefab, randomSpawnPoint.position, Quaternion.identity);
                //Instantiate(SpawnEffect, spawnable.prefab, randomSpawnPoint.position, Quaternion.identity);
                spawnedObject.transform.parent = transform; // Parent the spawned object to the spawner
                break;
            }

            spawnChance -= spawnable.spawnChance;
        }
    }

    private void IncreaseSpawnRate()
    {
        // Gradually increase the time between spawns up to the max time
        float progress = Mathf.Clamp01(gameTimer / timeToIncreaseSpawn);
        timeBetweenSpawns = Mathf.Lerp(initialTimeBetweenSpawns, maxTimeBetweenSpawns, progress);
    }
}
