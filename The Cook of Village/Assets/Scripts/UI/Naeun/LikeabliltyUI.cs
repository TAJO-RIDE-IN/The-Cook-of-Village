using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeabliltyUI : UIController
{
    public void LikeabliltyUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
