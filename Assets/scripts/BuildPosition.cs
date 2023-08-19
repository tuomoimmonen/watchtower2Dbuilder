using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPosition : MonoBehaviour
{
    BuildManager buildManager;
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
            Instantiate(buildManager.buildingToPlace, transform.position, transform.rotation);
            buildManager.buildingToPlace = null; //not able to continue spawning same buildings
            Destroy(gameObject); //not to allow multiple buildings
        }
    }
}
