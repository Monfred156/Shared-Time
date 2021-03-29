using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public Transform Target;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(Target.position.x, minX, maxX),
            Mathf.Clamp(Target.position.y, minY, maxY), transform.position.z);
    }
}