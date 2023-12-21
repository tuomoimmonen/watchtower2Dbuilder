using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public GameObject[] backgrounds;
    public float[] parallaxScales;
    public float smoothing = 1f;

    private Vector3[] previousBackgroundPositions;

    void Start()
    {
        previousBackgroundPositions = new Vector3[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            previousBackgroundPositions[i] = backgrounds[i].transform.position;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousBackgroundPositions[i].x - backgrounds[i].transform.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].transform.position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            backgrounds[i].transform.position = Vector3.Lerp(backgrounds[i].transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
            previousBackgroundPositions[i] = backgrounds[i].transform.position;
        }
    }
}
