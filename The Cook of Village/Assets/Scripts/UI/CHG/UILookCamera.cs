using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookCamera : MonoBehaviour
{
    
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
