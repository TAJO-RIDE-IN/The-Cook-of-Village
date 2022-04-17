using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingTool : MonoBehaviour
{
    public enum Type { Blender, Pot, Frypan }
    public Type type;
    
    public GameObject Inven;
    
    public List<int> ingredientList = new List<int>();
    public MaterialInfos currentMaterialInTool; //이걸로 리스트를 아예 만들어서 3개Infos를 다 저장해놓을지 ID만 3개 저장해놓을지 추후 수정

    public void PutIngredient()
    {
        if (ingredientList.Count == 0)
        {
            if (currentMaterialInTool != null)
            {
                ingredientList.Add(currentMaterialInTool.ID);
                Inven.SetActive(true);
                Inven.transform.GetChild(0).transform.GetComponent<Image>().sprite = currentMaterialInTool.ImageUI;
            }
            
        }
        else if(ingredientList.Count == 1)
        {
            if (currentMaterialInTool != null)
            {
                ingredientList.Add(currentMaterialInTool.ID);
                Inven.SetActive(true);
                Inven.transform.GetChild(0).transform.GetComponent<Image>().sprite = currentMaterialInTool.ImageUI;
            }
        }
        else if(ingredientList.Count == 2)
        {
            if (currentMaterialInTool != null)
            {
                ingredientList.Add(currentMaterialInTool.ID);
                Inven.SetActive(true);
                Inven.transform.GetChild(0).transform.GetComponent<Image>().sprite = currentMaterialInTool.ImageUI;
            }
        }
    }

}
