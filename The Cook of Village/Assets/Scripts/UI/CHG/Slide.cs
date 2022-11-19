using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Slide : MonoBehaviour
{
    [Range(0, 1)] 
    public float TweenSpeed;
    public LeanTweenType TweenType;

    private Canvas Canvas;
    private RectTransform rect;
    private float outPos;
    private float inpos;
    private bool isTweenedIn = true;
    private bool tweenComplete = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
