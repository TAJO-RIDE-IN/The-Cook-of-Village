using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookCameraRotation : MonoBehaviour
{
    
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(30, Camera.main.transform.eulerAngles.y, 0);
    }
}
