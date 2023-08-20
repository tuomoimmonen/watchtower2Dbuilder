using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    Transform target;
    Rigidbody2D bulletRb;
    ResourceManager manager;

    TurretManager turretManager;
    AsteroidSpawner spawner;
    AsteroidHealth asteroidHealth;

    AudioSource shootSound;
    void Start()
    {
        shootSound = GetComponent<AudioSource>();
        //asteroidHealth = FindObjectOfType<AsteroidHealth>();
        //spawner = FindObjectOfType<AsteroidSpawner>();
        manager = FindObjectOfType<ResourceManager>();
        bulletRb = GetComponent<Rigidbody2D>();
        turretManager = FindObjectOfType<TurretManager>();
        /*
        target = turretManager.target;

        Vector3 direction = target.position - transform.position;
        bulletRb.velocity = new Vector2(direction.x, direction.y).normalized * movementSpeed;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation+90);
        */
        PlayShootSound();
    }

    void Update()
    {
        //if (target == null) Destroy(gameObject);
        //transform.Translate(transform.up *  movementSpeed * Time.deltaTime);
        //transform.position += Vector3.up * movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            asteroidHealth = collision.GetComponent<AsteroidHealth>();
            asteroidHealth.TakeDamage(1);
            //spawner.enemiesAlive--;
            manager.buildingMaterial += 1;
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform _target) //call from turret and set the turrets target
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) { return; }
        Vector2 direction = (target.position - transform.position).normalized;
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
        bulletRb.velocity = direction * movementSpeed;
    }

    private void PlayShootSound()
    {
        float randomPitch = Random.Range(0.5f, 1.5f);
        shootSound.pitch = randomPitch;
        shootSound.Play();
    }
}
