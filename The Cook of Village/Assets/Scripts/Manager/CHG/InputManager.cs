using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    

    // Gamemanager.NextSceneIndex에 따라서 다르게 해줘야할듯
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            
            return;
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            return;
        }

    }
}
