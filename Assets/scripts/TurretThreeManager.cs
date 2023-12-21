using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretThreeController : MonoBehaviour
{
    public float range = 10f;
    public float rotationSpeed = 5f;
    public float fireRate = 1f;
    public int burstAmount = 3;
    public float reloadTime = 2f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform target;
    private float fireTimer;
    private int shotsFired;
    private float reloadTimer;
    private bool isReloading;

    BulletManager bulletManager;

    [SerializeField] int level = 0;
    [SerializeField] int maxLevel = 20;
    [SerializeField] int upgradeCost = 2;
    [SerializeField] float upgradeIncrease = 0.9f;

    ResourceManager manager;
    BuildManager buildManager;

    [SerializeField] GameObject upgradeButton;

    public bool canUpgrade = false;

    AudioSource upgradeAudioSource;

    private void Start()
    {
        upgradeAudioSource = GetComponent<AudioSource>();
        manager = FindObjectOfType<ResourceManager>();
        buildManager = FindObjectOfType<BuildManager>();
    }

    void Update()
    {
        UpgradeTurretUI();
        FindTarget();
        if (target != null)
        {
            RotateTurret();
            Shoot();
        }
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
        reloadTime *= upgradeIncrease;
        fireRate += upgradeIncrease;
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

    void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
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

    void RotateTurret()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    void Shoot()
    {
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                isReloading = false;
                reloadTimer = 0f;
            }
            return;
        }

        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            BulletManager bulletScript = projectile.GetComponent<BulletManager>();
            bulletScript.SetTarget(target);
            rb.AddForce(firePoint.right * 500f);
            fireTimer = 0f;
            shotsFired++;

            if (shotsFired >= burstAmount)
            {
                shotsFired = 0;
                isReloading = true;
            }
        }
    }
}
