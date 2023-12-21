using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] spawnObjects;

    [SerializeField] public float timeBetweenSpawns = 2f;
    [SerializeField] float difficultyIncrease = 0.75f;

    float difficultyTimer;
    float timeToSpawn;

    [SerializeField] int maxWave = 3;
    [SerializeField] int enemiesPerWave = 5;
    [SerializeField] float timeBetweenWaves = 5f;
    public int enemiesAlive; //control when the next wave starts
    public int currentWave;

    [SerializeField] TMP_Text waveAnnouncerText;
    [SerializeField] float announcerDuration = 2f;
    [SerializeField] float timeBeforeWave = 2f;
    private bool waveInProgress = false;

    Planet planet;

    void Start()
    {
        planet = FindObjectOfType<Planet>();
        enemiesAlive = 0;
        currentWave = 0;
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        /*
        difficultyTimer += Time.deltaTime;
        //timer
        if(Time.time > timeBetweenSpawns + timeToSpawn) 
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            int randomEnemy = Random.Range(0, spawnObjects.Length);
            Instantiate(spawnObjects[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            timeToSpawn = Time.time;

            if(difficultyTimer > nextWave)
            {
                difficultyTimer = 0;
                IncreaseDifficulty();   
            }
        }
        */

        /*
        if(enemiesAlive == 0 && currentWave <= maxWave)
        {
            StartCoroutine(WaveAnnouncer());
            StartCoroutine(SpawnWave());
        }
        */
    }

    private IEnumerator SpawnWaves()
    {
        //StartCoroutine(WaveAnnouncer());
        yield return new WaitForSeconds(timeBeforeWave);
        while(planet.isAlive == true)
        {
            if(enemiesAlive == 0 && !waveInProgress)
            {
                waveInProgress = true;
                yield return StartCoroutine(SpawnWave());
                waveInProgress = false;
            }
            yield return null;
        }
        
    }

    private IEnumerator SpawnWave()
    {
        waveAnnouncerText.gameObject.SetActive(true);
        waveAnnouncerText.text = "Wave " + (currentWave + 1) + " starting!";
        yield return new WaitForSeconds(announcerDuration);
        waveAnnouncerText.gameObject.SetActive(false);
        if(currentWave > 2)
        {
            enemiesPerWave++;
        }
        StartCoroutine(SpawnEnemy());
        currentWave++;
        maxWave++;
        yield return new WaitForSeconds(timeBetweenWaves);
    }
    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            int randomEnemy = Random.Range(0, spawnObjects.Length);
            Instantiate(spawnObjects[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            enemiesAlive++;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

    }

    private IEnumerator WaveAnnouncer()
    {
        waveAnnouncerText.gameObject.SetActive(true);
        waveAnnouncerText.text = "Wave " + (currentWave + 1) + " starting!";
        yield return new WaitForSeconds(announcerDuration);
        waveAnnouncerText.gameObject.SetActive(false);
    }

    private void IncreaseDifficulty()
    {
        if(timeBetweenSpawns > 0.75f)
        {
            timeBetweenSpawns *= difficultyIncrease;
        }
    }
}
