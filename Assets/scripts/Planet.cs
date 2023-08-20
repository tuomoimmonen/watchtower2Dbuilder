using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public int planetHealth = 3;
    [SerializeField] Image[] planetHealthUI;
    void Start()
    {
        //UpdateHealthUI();
    }

    void Update()
    {
        //UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = planetHealth; i < planetHealthUI.Length; i++)
        {
            planetHealthUI[i].gameObject.SetActive(false);
        }

        if(planetHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
             planetHealth--;
             UpdateHealthUI();
        }
    }
}
