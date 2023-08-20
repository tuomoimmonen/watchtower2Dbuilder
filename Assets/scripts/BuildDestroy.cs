using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDestroy : MonoBehaviour
{
    public GameObject buildPosition;

    [Header("Building cost")]
    public int buildingMaterialCost;

    public float timeBetweenIncomes; //how often we generate income
    float nextIncomeTime; //timer

    ResourceManager resourceManager;
    BuildManager buildManager;

    [SerializeField] GameObject destructionSound;

    void Start()
    {
        buildManager = FindObjectOfType<BuildManager>();
        resourceManager = FindObjectOfType<ResourceManager>(); //buildings are instansiated, need to find resourcemanager
        //remove the building cost when spawned
        resourceManager.buildingMaterial -= buildingMaterialCost;
    }

    void Update()
    {
        
    }

    private void OnMouseDown() //when clicked destroy building
    {
        //refund half of the buildings cost when deleting
        resourceManager.buildingMaterial += buildingMaterialCost / 2;
        buildManager.DecreaseTurretAmount();
        Instantiate(destructionSound,transform.position, Quaternion.identity); //sound here
        //instantiate new building position from prefab
        Instantiate(buildPosition, transform.position, transform.rotation);
        Destroy(gameObject); //destroy building
    }
}
