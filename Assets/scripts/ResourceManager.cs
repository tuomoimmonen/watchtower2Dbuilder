using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [Header("Resources")]
    public int buildingMaterial = 0;

    [Header("Resource texts")]
    public TMP_Text buildingMaterialText;

    TurretManager turretManager;

    void Start()
    {
    }

    void Update()
    {
        //keep the resource texts updated
        buildingMaterialText.text = buildingMaterial.ToString();
    }
}
