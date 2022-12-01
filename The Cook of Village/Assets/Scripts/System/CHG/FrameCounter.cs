using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FrameCounter : MonoBehaviour
{
    private float deltaTime = 0f;

    [SerializeField, Range(1, 100)] private int size = 25;

    [SerializeField] private Color color = Color.green;

    public float fps;
    public bool isShow;
    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledTime - deltaTime) * 0.1f;
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isShow = !isShow;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Application.targetFrameRate = 30;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Application.targetFrameRate = 60;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.targetFrameRate = 144;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Application.targetFrameRate = -1;
        }
    }

    private void OnGUI()
    {
        if (isShow)
        {
            GUIStyle style = new GUIStyle();
            
            Rect rect = new Rect(30, 30, Screen.width, Screen.height);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = size;
            style.normal.textColor = color;
            
            float ms = deltaTime * 1000f;
            fps = 1.0f / Time.deltaTime;
            string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);
            
            GUI.Label(rect, text, style);
        }
    }
}
