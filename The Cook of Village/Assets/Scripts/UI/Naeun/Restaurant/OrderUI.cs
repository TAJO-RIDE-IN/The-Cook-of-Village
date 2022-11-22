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
        }
    }

    public Image TimeBar;
    public Image FoodImage;
    public Image ToolImgae;
    public Image TongsImage;
    public Image VillageNPCImage;

    public List<Image> IngredientImage = new List<Image>();
    public List<Sprite> TongsSprite = new List<Sprite>();

    public Transform Order;
    public Transform OrderContainer;

    private ImageData imageData;
    private Color UIColor;
    private Color DefaultColor = new Color(1, 1, 1, 1);

    [SerializeField]
    private Animator ani;
    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        OrderAnimation(false);
        UIColor = DefaultColor;
        VillageNPCImage.gameObject.SetActive(false);
        foreach (var image in IngredientImage)
        {
            image.gameObject.SetActive(false);
        }
    }
    public void ModifyOrderUI(FoodInfos infos, bool Favorite = false, VillageGuest guest = null)
    {
        imageData = ImageData.Instance;
        foodInfos = infos;
        this.gameObject.transform.SetParent(Order);
        this.gameObject.SetActive(true);
        if(Favorite)
        {
            VillageNPCImageState(guest);
        }
        OrderContainer.gameObject.GetComponent<OrderControl>().OrderCount++;
        ChangeImage(imageData.FindImageData(food.ImageID), imageData.FindImageData(FoodData.Instance.foodTool[food.Type].ImageID));
        IngredientState();
    }
    private void VillageNPCImageState(VillageGuest guest = null)
    {
        if (guest != null)
        {
            VillageNPCImage.gameObject.SetActive(true);
            VillageNPCImage.sprite = imageData.FindImageData(guest.npcInfos.ImageID);
            if (!guest.npcInfos.EatFavriteFood)
            {
                UIColor = guest.VillageFoodColor;
            }
        }
    }
    private void IngredientState()
    {
        for(int i = 0; i < food.Recipe.Count; i++)
        {
            IngredientImage[i].gameObject.SetActive(true);
            IngredientImage[i].sprite = imageData.FindImageData(ItemData.Instance.ItemInfos(food.Recipe[i]).ImageID);
            IngredientImage[i].color = UIColor;
        }
    }
    private void ChangeImage(Sprite food, Sprite tool)
    {
        FoodImage.sprite = food;
        ToolImgae.sprite = tool;
        FoodImage.color = UIColor;
        ToolImgae.color = UIColor;
        TongsImage.sprite = TongsSprite[Random.Range(0, TongsSprite.Count)];
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
