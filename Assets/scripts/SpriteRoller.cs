using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRoller : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1.0f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
}
