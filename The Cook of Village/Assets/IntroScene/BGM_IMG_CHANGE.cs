using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
 
public class BGM_IMG_CHANGE : MonoBehaviour
{
    public Image TestImage; 
    public Sprite TestSprite; 
 
    public void ChangeImage()
    {
        TestImage.sprite = TestSprite; 
    }
}

