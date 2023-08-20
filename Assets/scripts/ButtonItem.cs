using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItem : MonoBehaviour
{
    public BuildDestroy buildDestroy;
    [SerializeField] ResourceManager resourceManager;
    AudioSource audioSource;
    Button button;
    [SerializeField] GameObject[] buildHereArrows;

    BuildManager manager;


    void Start()
    {
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
        }
        else
        {
            button.interactable = true;
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
