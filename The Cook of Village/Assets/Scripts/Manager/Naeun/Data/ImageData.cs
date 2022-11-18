using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageData : Singletion<ImageData>
{
    public Sprite[] ImageContainer;
    public Sprite FindImageData(int ImageID)
    {
        Sprite image = ImageContainer[ImageID];
        return image;
    }
}
