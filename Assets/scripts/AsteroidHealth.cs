using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    public float maxHealth = 5;
    private float currentHealth;

    AsteroidSpawner spawner;

    AudioSource hitSound;

    ParticleSystem hitEffect;

    [SerializeField] GameObject destroyEffect;

    void Start()
    {
        hitEffect = GetComponent<ParticleSystem>();
        hitSound = GetComponent<AudioSource>();
        spawner = FindObjectOfType<AsteroidSpawner>();
        currentHealth = maxHealth;
        UpdateHealthBar();
        
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            spawner.enemiesAlive--;
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        hitEffect.Play();
        PlayHitSound();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthSlider.value = healthPercentage;
    }

    private void PlayHitSound()
    {
        float randomPitch = Random.Range(0.8f, 1.2f);
        hitSound.volume = 0.1f;
        hitSound.pitch = randomPitch;
        hitSound.Play();
    }
}
