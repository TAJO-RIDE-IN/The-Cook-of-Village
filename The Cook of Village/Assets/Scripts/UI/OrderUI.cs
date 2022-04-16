/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    private FoodInfos food;
    public FoodInfos foodInfos
    {
        get { return food; }
        set
        {
            food = value;
            ChangeImage(food.ImageUI, FoodData.Instance.foodTool[food.Type].ToolImage);
            MateiralState();
            StartCoroutine(WaitingOrder());
        }
    }

    public Image TimeBar;
    public Image FoodImage;
    public Image ToolImgae;

    public GameObject Material;
    public List<GameObject> MaterialObject = new List<GameObject>();
    public List<Image> MaterialImage = new List<Image>();

    private void OnDisable()
    {
        foreach(GameObject i in MaterialObject)
        {
            i.SetActive(false);
        }
    }
    private void MateiralState()
    {
        for(int i = 0; i < foodInfos.Recipe.Count; i++)
        {
            MaterialObject[i].SetActive(true);
            MaterialImage[i].sprite = MaterialData.Instance.materialInfos(foodInfos.Recipe[i]).ImageUI;
        }
    }
    private void ChangeImage(Sprite food, Sprite tool)
    {
        FoodImage.sprite = food;
        ToolImgae.sprite = tool;
    }
    private IEnumerator WaitingOrder()
    {
        float time = FoodData.Instance.WaitingTime;
        while (time > 0)
        {
            time -= Time.deltaTime;
            TimeBar.fillAmount = time / FoodData.Instance.WaitingTime;
            yield return null;
            if (time <= 0)
            {
                EndOrder();
            }
        }
    }
    private void EndOrder()
    {
        ObjectPooling<OrderUI>.ReturnObject(this);
    }
}
