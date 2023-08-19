using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [Header("Resources")]
    public float buildingMaterial = 0;
    public float electricity = 0;
    //public float wood;
    //public float gem;

    [Header("Resource texts")]
    public TMP_Text buildingMaterialText;
    public TMP_Text electricityText;
    //public TMP_Text woodText;
    //public TMP_Text gemText;

    TurretManager turretManager;

    void Start()
    {
    }

    void Update()
    {

        //keep the resource texts updated
        buildingMaterialText.text = buildingMaterial.ToString();
        electricityText.text = electricity.ToString();
        //woodText.text = wood.ToString();
        //gemText.text = gem.ToString();

    }
}
