using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Object = System.Object;

public class CookingTool : MonoBehaviour
{

    //public enum ToolID { Blender = 0, Pot = 1, FryPan = 2, Whipper = 3, Oven = 4}
    public Gradient gradient;
    public FoodTool.Type toolID;
    public GameObject InventoryBig;
    public GameObject IngredientInven;
    
    public Image[] Ing = new Image[3];
    public Image food;
    public Image foodBig;
    public Image greenCircle;
    public Image greenCircleBig;

    public GameObject circleUI;
    public GameObject circleUIBig;
    public Sprite toolBeforeCook;
    public CookItemSlotManager cookSlotManager;
    public float GreenPotionEffect = 1f;
    
    public List<int> ingredientList = new List<int>();//?¥Í±¥ ?îÎ¶¨???åÎßå ?¨Ïö©, ?∏Îç±?§Í? ?ÑÏöî??ID??CookItemSlot???Ä??
    public FoodInfos FoodInfos { get; set;}//foodInfosÍ∞Ä Î∞îÎÄåÎ©¥ ?¥Ï§Ñ ??Ï¶?UIÏΩîÎ£®???ùÎÇ¨?ÑÎïå ????set???ÅÏûê
    
    [HideInInspector]public bool isBeforeCooking = true;//?îÎ¶¨Î•??úÏûë?òÎ©¥ falseÍ∞Ä ?òÍ≥†, ?îÎ¶¨Í∞Ä ?ùÎÇòÎ©?trueÍ∞Ä ?úÎã§.
    [HideInInspector]public bool isCooked;//?îÎ¶¨Í∞Ä ?ÑÏÑ±?òÎ©¥ trueÍ∞Ä ?òÍ≥†, ?îÎ¶¨Í∞Ä ?¥Í≤®?àÏ? ?äÏúºÎ©?false?¥Îã§.
    public bool isPlayer;
    public int index;
    
    ItemData item = ItemData.Instance;
    
    private Animation _animation;
    private float currentValue;
    private IEnumerator _burntCoroutine;
    private Vector3 cameraVector;
    private ToolPooling toolPooling;
    private SoundManager _soundManager;

    

    /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad(){
        
    }*/
    
    private void Start()
    {
        //InventoryBig.SetActive(false);
        _soundManager = SoundManager.Instance;
        toolPooling = ToolPooling.Instance;
        _animation = transform.GetComponent<Animation>();
        _burntCoroutine = BurntFood();
        /*if (toolID != FoodTool.Type.Oven)
        {
            InventoryBig.SetActive(true);
        }*/
        //InventoryBig.transform.localScale = Vector2.zero;
    }

    public void OpenBigUI()
    {
        
    }

    
    public void ReturnIngredient(int i)
    {
        if (ingredientList.Count > 0)
        {
            if (ChefInventory.Instance.AddIngredient(ItemData.Instance.ItemInfos(cookSlotManager.itemslots[i].ingridientId)))
            {
                cookSlotManager.itemslots[i].isUsed = false;
                Ing[i].sprite = cookSlotManager.emptyInven;
                ingredientList.Remove(cookSlotManager.itemslots[i].ingridientId);
                cookSlotManager.itemslots[i].ChangeSlotUI(cookSlotManager.emptyInven);
            }
            
        }
    }

    public bool PutIngredient(int id, Sprite sprite) //?¥Í±∏ ?ÑÏû¨ ?§Í≥†?àÎäîÍ≤?null???ÑÎãê?åÎßå ?§Ìñâ?úÏºúÏ£ºÎ©¥ ?òÎäî???πÏãúÎ™∞Îùº???úÎ≤à ??Ï°∞Í±¥Î¨??£Ïùå
    {
        if (isBeforeCooking)
        {
            if (!isCooked)
            {
                for (int i = 0; i < cookSlotManager.ChildSlotCount; i++) //?ºÎã® ?àÏãú?ºÏóê ?§Ïñ¥Í∞Ä??ÏµúÎ? ?¨Î£å Í∞úÏàòÍ∞Ä 3Í∞úÎùºÍ≥??àÏùÑ ??
                {
                    if (!cookSlotManager.itemslots[i].isUsed)
                    {
                        if (toolID != FoodTool.Type.Plate)
                        {
                            _animation.Play(toolID.ToString());
                        }
                        
                        
                        ingredientList.Add(id);
                        cookSlotManager.itemslots[i].isUsed = true;
                        cookSlotManager.itemslots[i].ChangeSlotUI(sprite);
                        cookSlotManager.itemslots[i].ingridientId = id;
                        Ing[i].sprite = sprite;
                        IngredientInven.SetActive(true);
                        return true;
                    }
            
                }
                cookSlotManager.ShowWarning();
            }
        }
        return false;
    }

    public void Cook()
    {
        if (isBeforeCooking)
        {
            if (!isCooked)
            {
                if (ingredientList.Count > 0)
                {
                    _soundManager.PlayEffect3D(_soundManager._audioClips[toolID.ToString()], gameObject, true);
                    isBeforeCooking = false;
                    ingredientList.Sort();
                    FoodInfos = FoodData.Instance.RecipeFood((int)toolID, ingredientList);
                    cookSlotManager.RefreshSlot();
                    RemoveIngSlot();
                    ingredientList.Clear();
                    //Debug.Log(FoodInfos.Name);
                    IngredientInven.SetActive(false);
                    StartCoroutine(CookingGauge());
                    return;
                }
                else
                {
                    //?¨Î£åÎ•??£Ïúº?∏Ïöî! UI Ï∂úÎ†•
                }
            }
            
        }
    }

    public void RemoveFood()
    {
        currentValue = 0;
        isCooked = false;
        food.sprite = toolBeforeCook;
        StopCoroutine(_burntCoroutine);
        if (toolID == FoodTool.Type.Plate)
        {
            WhenReturn();
            toolPooling.toolInstallMode.ActviePositionCollider(index);
            toolPooling.ReturnObject(this, toolID.ToString());
            toolPooling.toolInstallMode.isUsed[index] = false;
        }
    }

    public void RemoveIngSlot()
    {
        for (int i = 0; i < 3; i++)
        {
            Ing[i].sprite = cookSlotManager.emptyInven;
        }
    }

    public void OpenUI(float time)
    {
        InventoryBig.SetActive(true);
        UIManager.UIScalePunchAnimation(InventoryBig);
        //InventoryBig.LeanScale(Vector3.one, time).setEaseOutElastic();
    }

    public void CloseUI()
    {
        if (toolPooling.toolInstallMode.isDirectChange)
        {
            //InventoryBig.LeanScale(Vector2.zero, 0f);
            InventoryBig.SetActive(false);
            ChefInventory.Instance._cookingCharacter.isSpace = false;
            return;
        }
        InventoryBig.SetActive(false);

        //InventoryBig.LeanScale(Vector2.zero, 1f).setEaseInBack().setOnComplete(() => );

    }
    /// <summary>
    /// Î∞îÎ°ú ?îÎ¶¨?ÑÍµ¨Î•??ÜÏï†Í≥??§Ïπò?òÍ∏∞ ?ÑÌïú ?§Ï†ï
    /// </summary>
    public void DirectSetUp()
    {
        toolPooling.toolInstallMode.DirectChange();
        toolPooling.indexToChange = index;
        CloseUI();
    }

    /// <summary>
    /// ?ÄÎßÅÎèÑ ?åÎ†§Ï£ºÍ≥†, FoodData Amount?Ä ItemData Amount??Î∞îÍøîÏ§?
    /// </summary>
    public void DeleteTool() 
    {
        WhenReturn();
        InstallData.Instance.DeleteIndexData(index, InstallData.SortOfInstall.Tool);
        toolPooling.toolInstallMode._cookingCharacter.isToolCollider = false;
        toolPooling.toolInstallMode.PositionCollider[index].SetActive(true);
        toolPooling.toolInstallMode.PositionCanvas[index].SetActive(false);
        toolPooling.toolInstallMode.ActviePositionCollider(index);
        toolPooling.toolInstallMode.isUsed[index] = false;
        if (!toolPooling.toolInstallMode.isDirectChange)
        {
            toolPooling.toolInstallMode._cookingCharacter._cookPosition =
                toolPooling.toolInstallMode.PositionCollider[index].GetComponent<CookPosition>();
            toolPooling.toolInstallMode._cookingCharacter._cookPosition.OpenUI(0);
        }
        toolPooling.ChangeToolAmount(1, toolID);
        toolPooling.ReturnObject(this, toolID.ToString());
        


    }

    private void WhenReturn()
    {
        InventoryBig.SetActive(false);
        IngredientInven.SetActive(false);
        RemoveIngSlot();
        ingredientList.Clear();
    }
    
    IEnumerator CookingGauge() //LoadingBar.fillAmount??1???†ÎïåÍπåÏ? ?êÏ†ê Í≤åÏù¥ÏßÄÎ•?Ï∂îÍ??¥Ï§å
    {
        greenCircle.fillAmount = 0;
        while (greenCircle.fillAmount < 1)
        {
            currentValue += Time.deltaTime;
            greenCircle.fillAmount = currentValue / FoodInfos.MakeTime * GreenPotionEffect;
            greenCircleBig.fillAmount = currentValue / FoodInfos.MakeTime * GreenPotionEffect;
            circleUI.transform.Rotate(0, 0, 1);
            circleUIBig.transform.Rotate(0, 0, 1);
            yield return null;
        }
        _soundManager.StopEffect3D(gameObject);
        isBeforeCooking = true;
        currentValue = 0;
        isCooked = true;
        food.sprite = ImageData.Instance.FindImageData(FoodInfos.ImageID);
        foodBig.sprite = ImageData.Instance.FindImageData(FoodInfos.ImageID);
        //_soundManager.Play(_soundManager._audioClips[]);
        if (FoodInfos.ID != 100000)
        {
            _burntCoroutine = BurntFood();
            StartCoroutine(_burntCoroutine);
        }
    }

    IEnumerator BurntFood()
    {
        while (greenCircle.fillAmount > 0)
        {
            //Debug.Log(currentValue / FoodInfos.MakeTime * 1.25f);
            currentValue += Time.deltaTime;
            greenCircle.fillAmount = 1 - (currentValue / (FoodInfos.BurntTime + 15f));
            greenCircleBig.fillAmount = 1 - (currentValue / (FoodInfos.BurntTime + 15f));
            greenCircle.color = gradient.Evaluate(1 - greenCircle.fillAmount);
            greenCircleBig.color = gradient.Evaluate(1 - greenCircleBig.fillAmount);
            yield return null;
        }
        currentValue = 0;
        food.sprite = ImageData.Instance.FindImageData(FoodData.Instance.foodTool[6].foodInfos[1].ImageID);
        foodBig.sprite = ImageData.Instance.FindImageData(FoodData.Instance.foodTool[6].foodInfos[1].ImageID); 
        FoodInfos = FoodData.Instance.foodTool[6].foodInfos[1];

    }

    private void OnCollisionStay(Collision other)
    {
        if(other.transform.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.transform.CompareTag("Player"))
        {
            isPlayer = false;
            Debug.Log("?åÎ†à?¥Ïñ¥?òÍ∞ê");
        }
        
    }
}
