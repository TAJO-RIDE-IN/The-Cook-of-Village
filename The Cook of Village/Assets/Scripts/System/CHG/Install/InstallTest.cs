using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallTest : MonoBehaviour
{
    public GameObject beforeObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                beforeObject.transform.position = hit.point;
            }
        }
    }
}
