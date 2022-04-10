using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextChange : MonoBehaviour
{
    public float time;
    public Text text;
    double num;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
	num = (int)time;
	switch (num)
	{
	    	case 1:
		{
			text.text = "Loading";
			break;
		}
 		case 2:
		{
			text.text = "Loading •";
			break;
		}
		case 3:
		{
			text.text = "Loading ••";
			break;
		}
 		case 4:
		{
			text.text = "Loading •••";
			time = 0;
			break;
		}

    	}
   }

}
