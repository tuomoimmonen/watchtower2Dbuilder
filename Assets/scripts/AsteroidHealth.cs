using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    public int maxHealth = 5;
    private int currentHealth;

    AsteroidSpawner spawner;

    AudioSource hitSound;

    ParticleSystem hitEffect;

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
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
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
        hitSound.pitch = randomPitch;
        hitSound.Play();
    }
}
