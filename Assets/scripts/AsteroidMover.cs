using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    public int asteroidHealth = 2;
    private Transform target;

    ResourceManager manager;

    GameObject[] asteroidEndpoints;
 
    void Start()
    {
        asteroidEndpoints = GameObject.FindGameObjectsWithTag("Endpoint");
        //target = FindObjectOfType<Planet>().transform;
        int randomEndpoint = Random.Range(0, asteroidEndpoints.Length);
        target = asteroidEndpoints[randomEndpoint].transform;
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
            Destroy(gameObject);
        }
    }
}
