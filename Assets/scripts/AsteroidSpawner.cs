using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] spawnObjects;

    [SerializeField] float timeBetweenSpawns = 2f;
    float timeToSpawn;
    void Start()
    {
        
    }

    void Update()
    {

        //timer
        if(Time.time > timeBetweenSpawns + timeToSpawn) 
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            int randomEnemy = Random.Range(0, spawnObjects.Length);
            Instantiate(spawnObjects[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            timeToSpawn = Time.time;
        }
    }
}
