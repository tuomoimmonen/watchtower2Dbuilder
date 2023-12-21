using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour
{
    public Transform target;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] public float timeBetweenShots = 4f;
    [SerializeField] float turretRange = 8f;
    float timeToShoot;
    float startTimer = 2f;

    [SerializeField] LayerMask enemyMask;

    [SerializeField] public float rotationSpeed = 40f;

    [SerializeField] int level = 0;
    [SerializeField] int maxLevel = 20;
    [SerializeField] int upgradeCost = 2;
    [SerializeField] float upgradeIncrease = 0.9f;

    ResourceManager manager;
    BuildManager buildManager;

    [SerializeField] GameObject upgradeButton;

    public bool canUpgrade = false;

    AudioSource upgradeAudioSource;
    void Start()
    {
        manager = FindObjectOfType<ResourceManager>();
        buildManager = FindObjectOfType<BuildManager>();
        upgradeAudioSource = GetComponent<AudioSource>();
        //target = FindObjectOfType<AsteroidMover>().transform;
    }

    void Update()
    {
        
        UpgradeTurretUI();
        startTimer -= Time.deltaTime;
        Shoot();

    }

    private void UpgradeTurretUI()
    {
        if (manager.buildingMaterial >= upgradeCost)
        {
            canUpgrade = true;
        }
        else { canUpgrade = false; }

        if (canUpgrade == true && level < maxLevel)
        {
            upgradeButton.SetActive(true);
        }
        else { upgradeButton.SetActive(false); }
    }

    public void UpgradeTurret()
    {
        level++;
        timeBetweenShots *= upgradeIncrease;
        rotationSpeed += upgradeIncrease;
        manager.buildingMaterial -= upgradeCost;
        upgradeCost += Mathf.RoundToInt(upgradeIncrease);
        upgradeButton.SetActive(false);
    }

    public void PlayPopSound()
    {
        //random pitch
        float randomPitch = Random.Range(0.5f, 1.5f);
        upgradeAudioSource.pitch = randomPitch;
        upgradeAudioSource.Play();
    }

    private void Shoot()
    {
        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle + 90);
            //transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            if (distance < turretRange)
            {
                if ((Time.time > timeBetweenShots + timeToShoot) && (startTimer <= 0))
                {
                    GameObject bulletObj = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                    BulletManager bulletScript = bulletObj.GetComponent<BulletManager>();
                    bulletScript.SetTarget(target);
                    timeToShoot = Time.time;
                }
            }

        }
        else
        {
            FindTarget();
            //target = FindObjectOfType<AsteroidMover>().transform;
            return;
        }
    }

    /*
    public void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 10f, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[Random.Range(0, hits.Length)].transform;
        }
        //else { target = null; }
    }
    */

    void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, turretRange);
        float minDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = collider.transform;
                }
            }
        }

        target = closestTarget;
    }
}
