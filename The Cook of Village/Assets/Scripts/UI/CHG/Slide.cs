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
        transform.localScale = Vector2.zero;
        rect = GetComponent<RectTransform>();
    }

    public void Open()
    {
        transform.LeanScale(Vector3.one, 1f);
    }
    public void Close()
    {
        transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    }

    
}
