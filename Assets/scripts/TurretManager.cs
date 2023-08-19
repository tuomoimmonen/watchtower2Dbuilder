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
    [SerializeField] int maxLevel = 10;
    [SerializeField] float upgradeCost = 2;
    [SerializeField] float upgradeIncrease = 0.9f;

    ResourceManager manager;
    BuildManager buildManager;

    [SerializeField] GameObject upgradeButton;

    public bool canUpgrade = false;
    void Start()
    {
        manager = FindObjectOfType<ResourceManager>();
        buildManager = FindObjectOfType<BuildManager>();
        //target = FindObjectOfType<AsteroidMover>().transform;
    }

    void Update()
    {
        if (manager.buildingMaterial >= upgradeCost)
        {
            canUpgrade = true;
        }
        else { canUpgrade = false; }

        startTimer -= Time.deltaTime;
        Shoot();
        if(canUpgrade == true && level < maxLevel)
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
        upgradeCost += upgradeIncrease;
        upgradeButton.SetActive(false);
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

    public void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 10f, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[Random.Range(0, hits.Length)].transform;
        }
        //else { target = null; }
    }
}
