using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject[] buildings; //buildings to create
    public GameObject buildingToPlace; //what is the building to place

    [SerializeField] GameObject[] buildHereArrows;

    [SerializeField] public GameObject upgradeButton;

    public int turretAmount = 0;

    void Start()
    {

    }

    void Update()
    {
        BuildingInstructionPopUp();
    }

    public void OnClickButton(int index) //public to be able to call it from button
    {
        buildingToPlace = buildings[index]; //selected from the buildings array
    }

    void BuildingInstructionPopUp()
    {
        //building instruction here
        if (buildingToPlace == null)
        {
            for (int i = 0; i < buildHereArrows.Length; i++)
            {
                buildHereArrows[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < buildHereArrows.Length; i++)
            {
                buildHereArrows[i].gameObject.SetActive(true);
            }
        }
    }

    public void ShowUpgradeButton()
    {
        upgradeButton.SetActive(true);
    }

    public void IncreaseTurretAmount()
    {
        turretAmount++;
    }

    public void DecreaseTurretAmount()
    {
        turretAmount--;
    }
}
