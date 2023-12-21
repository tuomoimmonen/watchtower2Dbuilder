using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public int planetHealth = 3;
    [SerializeField] Image[] planetHealthUI;

    Animator animator;

    [SerializeField] GameObject deathEffect;

    public bool isAlive = true;

    AsteroidSpawner spawner;
    void Start()
    {
        spawner = FindObjectOfType<AsteroidSpawner>();
        animator = GetComponent<Animator>();
        //UpdateHealthUI();

    }

    void Update()
    {
        //UpdateHealthUI();
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            planetHealth = 0;
            UpdateHealthUI();
        }
        */
    }

    private void UpdateHealthUI()
    {
        for (int i = planetHealth; i < planetHealthUI.Length; i++)
        {
            planetHealthUI[i].gameObject.SetActive(false);
        }

        
        if(planetHealth <= 0)
        {
            animator.SetTrigger("death");
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            isAlive = false;
            StartCoroutine(MenuManager.instance.GameOver());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            animator.SetTrigger("hit");
            planetHealth--;
            UpdateHealthUI();
        }
    }
    

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("hit");
            Destroy(collision.gameObject);

            planetHealth--;
            UpdateHealthUI();
        }
    }
    */
}
