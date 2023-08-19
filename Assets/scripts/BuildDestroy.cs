using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDestroy : MonoBehaviour
{

    public GameObject buildPosition;

    [Header("Income from the building")]
    public float buildingMaterialIncome;
    public float electricityIncome;
    //public float woodIncome;
    //public float gemIncome;

    [Header("Building cost")]
    public float buildingMaterialCost;
    public float electricityCost;
    //public float woodCost;
    //public float gemCost;

    public float timeBetweenIncomes; //how often we generate income
    float nextIncomeTime; //timer

    ResourceManager resourceManager;

    [SerializeField] GameObject destructionSound;
    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>(); //buildings are instansiated, need to find resourcemanager
        //remove the building cost when spawned
        resourceManager.buildingMaterial -= buildingMaterialCost;
        resourceManager.electricity -= electricityCost;
        //resourceManager.wood -= woodCost;
        //resourceManager.gem -= gemCost;
    }

    void Update()
    {
        //timer to get currency
        if(Time.time > nextIncomeTime)
        {
            resourceManager.buildingMaterial += buildingMaterialIncome;
            resourceManager.electricity += electricityIncome;
            //resourceManager.wood += woodIncome;
            //resourceManager.gem += gemIncome;
            nextIncomeTime = Time.time + timeBetweenIncomes;
        }
        
    }

    private void OnMouseDown() //when clicked destroy building
    {
        //refund half of the buildings cost when deleting
        resourceManager.buildingMaterial += buildingMaterialCost / 2;
        resourceManager.electricity += electricityCost / 2;
        //resourceManager.wood += woodCost / 2;
        //resourceManager.gem += gemCost / 2;

        Instantiate(destructionSound,transform.position, Quaternion.identity); //sound here
        //instantiate new building position from prefab
        Instantiate(buildPosition, transform.position, transform.rotation);
        Destroy(gameObject); //destroy building
    }
}
