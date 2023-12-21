using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    [SerializeField] float movementSpeed = 0.2f;
    public int asteroidHealth = 2;
    private Transform target;

    ResourceManager manager;

    GameObject[] asteroidEndpoints;

    AsteroidSpawner spawner;
 
    void Start()
    {
        spawner = FindObjectOfType<AsteroidSpawner>();
        asteroidEndpoints = GameObject.FindGameObjectsWithTag("Endpoint");
        //target = FindObjectOfType<Planet>().transform;
        int randomEndpoint = Random.Range(0, asteroidEndpoints.Length);
        target = asteroidEndpoints[randomEndpoint].transform;

        IncreaseDifficulty();
    }

    private void IncreaseDifficulty()
    {
        switch (spawner.currentWave)
        {
            case < 2:
                movementSpeed = 0.3f;
                asteroidHealth = 2;
                spawner.timeBetweenSpawns *= 0.8f;
                break;
            case > 2 and <= 4:
                movementSpeed *= 2;
                asteroidHealth *= 2;
                spawner.timeBetweenSpawns *= 0.7f;
                break;
            case > 4 and <= 6:
                movementSpeed *= 3;
                asteroidHealth *= 3;
                spawner.timeBetweenSpawns *= 0.6f;
                break;
            case > 6 and <= 8:
                movementSpeed *= 5;
                asteroidHealth *= 5;
                spawner.timeBetweenSpawns *= 0.5f;
                break;
            case > 8 and <= 15:
                movementSpeed *= 7;
                asteroidHealth *= 7;
                spawner.timeBetweenSpawns *= 0.4f;
                break;
            case > 15 and <= 20:
                movementSpeed *= 9;
                asteroidHealth *= 9;
                spawner.timeBetweenSpawns *= 0.4f;
                break;
            case > 20:
                movementSpeed *= 12;
                asteroidHealth *= 12;
                spawner.timeBetweenSpawns *= 0.3f;
                break;

        }
        /*
        if (spawner.currentWave < 2)
        {
            movementSpeed = 0.2f;
            asteroidHealth = 2;
            spawner.timeBetweenSpawns *= 0.8f;
        }
        else if (spawner.currentWave > 2 && spawner.currentWave <= 4)
        {
            movementSpeed *= 2;
            asteroidHealth *= 2;
            spawner.timeBetweenSpawns *= 0.7f;
        }
        else if (spawner.currentWave > 4 && spawner.currentWave <= 6)
        {
            movementSpeed *= 4;
            asteroidHealth *= 4;
            spawner.timeBetweenSpawns *= 0.6f;
        }
        else if (spawner.currentWave > 6 && spawner.currentWave <= 8)
        {
            movementSpeed *= 8;
            asteroidHealth *= 8;
            spawner.timeBetweenSpawns *= 0.5f;
        }
        */
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);

        /*
        if(asteroidHealth == 0)
        {
            manager.buildingMaterial += 1;
            Destroy(gameObject);
        }
        */
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Planet>() == true)
        {
            spawner.enemiesAlive--;
            Destroy(gameObject);
        }
    }
    
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Planet"))
        {
            spawner.enemiesAlive--;
            Destroy(gameObject);
        }
    }
    */

    /*
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Planet")
        {
            Debug.Log(other.name);
            spawner.enemiesAlive--;
            Destroy(gameObject);
        }
    }
    */
    
    

}
