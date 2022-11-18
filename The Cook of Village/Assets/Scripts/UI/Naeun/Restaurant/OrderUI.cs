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
            this.gameObject.transform.SetParent(Order);
            this.gameObject.SetActive(true);
            OrderContainer.gameObject.GetComponent<OrderControl>().OrderCount++;
            ChangeImage(ImageData.Instance.FindImageData(food.ImageID), ImageData.Instance.FindImageData(FoodData.Instance.foodTool[food.Type].ImageID));
            MateiralState();
        }
    }

    public Image TimeBar;
    public Image FoodImage;
    public Image ToolImgae;

    public GameObject Material;
    public List<GameObject> MaterialObject = new List<GameObject>();
    public List<Image> MaterialImage = new List<Image>();

    public Transform Order;
    public Transform OrderContainer;

    [SerializeField]
    private Animator ani;
    private void OnDisable()
    {
        OrderAnimation(false);
        foreach (GameObject i in MaterialObject)
        {
            i.SetActive(false);
        }
    }
    private void MateiralState()
    {
        for(int i = 0; i < food.Recipe.Count; i++)
        {
            MaterialObject[i].SetActive(true);
            MaterialImage[i].sprite = ImageData.Instance.FindImageData(ItemData.Instance.ItemInfos(food.Recipe[i]).ImageID);
        }
    }
    private void ChangeImage(Sprite food, Sprite tool)
    {
        FoodImage.sprite = food;
        ToolImgae.sprite = tool;       
    }
    public void OrderAnimation(bool state)
    {
         ani.SetBool("TimeOut", state);
    }
    public void EndOrder()
    {
        this.gameObject.transform.SetParent(OrderContainer);
        OrderContainer.gameObject.GetComponent<OrderControl>().OrderCount--;
        ObjectPooling<OrderUI>.ReturnObject(this);
    }
}
