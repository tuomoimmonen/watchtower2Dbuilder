using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPosition : MonoBehaviour
{
    BuildManager buildManager;
    [SerializeField] Button destroyButton;
    void Start()
    {
        buildManager = FindObjectOfType<BuildManager>();
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(buildManager.buildingToPlace != null) //guard for error
        {
            buildManager.IncreaseTurretAmount();
            Instantiate(buildManager.buildingToPlace, transform.position, transform.rotation);
            buildManager.buildingToPlace = null; //not able to continue spawning same buildings     
            Destroy(gameObject); //not to allow multiple buildings
        }
    }
}
