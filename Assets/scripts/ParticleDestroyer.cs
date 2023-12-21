using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    AsteroidSpawner spawner;
    ParticleSystem particleSystem;

    [SerializeField] GameObject deathEffect;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        spawner = FindObjectOfType<AsteroidSpawner>();   
    }

    private void OnParticleCollision(GameObject other)
    {
        Instantiate(deathEffect, other.transform.position, other.transform.rotation);
        Destroy(other);
    }
}
