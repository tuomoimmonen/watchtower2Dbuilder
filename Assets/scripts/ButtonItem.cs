using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonItem : MonoBehaviour
{
    public BuildDestroy buildDestroy;
    [SerializeField] ResourceManager resourceManager;
    AudioSource audioSource;
    Button button;
    [SerializeField] GameObject[] buildHereArrows;
    [SerializeField] GameObject selectTurretText;
    [SerializeField] GameObject buildHereText;

    Animator animator;

    BuildManager manager;


    void Start()
    {
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<BuildManager>();
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        //if not enough resources make button not interactable
        if ((resourceManager.buildingMaterial < buildDestroy.buildingMaterialCost) || manager.turretAmount == 6)
        {
            button.interactable = false;
            animator.SetBool("isClickable", false);
            selectTurretText.SetActive(false);
            //buildHereText.SetActive(false);

        }
        else
        {
            button.interactable = true;
            animator.SetBool("isClickable", true);
            selectTurretText.SetActive(true);
        }

        if (manager.turretSelected == true)
        {
            selectTurretText.SetActive(false);
            buildHereText.SetActive(true);
        }
        else
        {
            buildHereText.SetActive(false);
        }
    }

    public void PlayPopSound()
    {
        //random pitch
        float randomPitch = Random.Range(0.5f, 1.5f);
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }
}
