using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Object = System.Object;

public class CookingTool : MonoBehaviour
{
    
    //public enum ToolID { Blender = 0, Pot = 1, FryPan = 2, Whipper = 3, Oven = 4}
    public FoodTool.Type toolID;
    public GameObject InventoryBig;
    public GameObject IngredientInven;
    
    public Image[] Ing = new Image[3];
    public Image food;
    public Image foodBig;
    public Image blackCircle;
    public Image blackCircleBig;
    public GameObject circleUI;
    public GameObject circleUIBig;
    public Sprite toolBeforeCook;
    public CookItemSlotManager cookSlotManager;
    public float GreenPotionEffect = 1f;
    
    public List<int> ingredientList = new List<int>();//이건 요리할 때만 사용, 인덱스가 필요한 ID는 CookItemSlot에 저장
    public FoodInfos FoodInfos { get; set;}//foodInfos가 바뀌면 해줄 일,즉 UI코루틴 끝났을때 할 일 set에 적자
    
    [HideInInspector]public bool isBeforeCooking = true;//요리를 시작하면 false가 되고, 요리가 끝나면 true가 된다.
    [HideInInspector]public bool isCooked;//요리가 완성되면 true가 되고, 요리가 담겨있지 않으면 false이다.
    public int index;
    
    ItemData item = ItemData.Instance;
    
    private Animation _animation;
    private float currentValue;
    private IEnumerator _burntCoroutine;
    private Vector3 cameraVector;
    private ToolPooling toolPooling;
    private SoundManager soundManager;

    

    /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad(){
        
    }*/
    
    private void Start()
    {
        //InventoryBig.SetActive(false);
        soundManager = SoundManager.Instance;
        toolPooling = ToolPooling.Instance;
        _animation = transform.GetComponent<Animation>();
        _burntCoroutine = BurntFood();
        if (toolID != FoodTool.Type.Oven)
        {
            InventoryBig.SetActive(true);
        }
        
        InventoryBig.transform.localScale = Vector2.zero;
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
                Ing[i].sprite = cookSlotManager.emptySlot;
                ingredientList.Remove(cookSlotManager.itemslots[i].ingridientId);
                cookSlotManager.itemslots[i].ChangeSlotUI(cookSlotManager.emptySlot);
            }
            
        }
    }

    public bool PutIngredient(int id, Sprite sprite) //이걸 현재 들고있는게 null이 아닐때만 실행시켜주면 되는데 혹시몰라서 한번 더 조건문 넣음
    {
        if (isBeforeCooking)
        {
            if (!isCooked)
            {
                for (int i = 0; i < cookSlotManager.ChildSlotCount; i++) //일단 레시피에 들어가는 최대 재료 개수가 3개라고 했을 때
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
                    soundManager.PlayEffect3D(soundManager._audioClips[toolID.ToString()], gameObject, true);
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
                    //재료를 넣으세요! UI 출력
                }
            }
            
        }
    }

    public void RemoveFood()
    {
        currentValue = 0;
        blackCircle.fillAmount = 0;
        blackCircleBig.fillAmount = 0;
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
            Ing[i].sprite = cookSlotManager.emptySlot;
        }
    }

    public void OpenUI(float time)
    {
        InventoryBig.LeanScale(Vector2.one, time).setEaseOutElastic();
    }

    public void CloseUI()
    {
        if (toolPooling.toolInstallMode.isDirectChange)
        {
            InventoryBig.LeanScale(Vector2.zero, 0f);
            ChefInventory.Instance._cookingCharacter.isSpace = false;
            return;
        }
        InventoryBig.LeanScale(Vector2.zero, 0.7f).setEaseInBack();
        
    }
    /// <summary>
    /// 바로 요리도구를 없애고 설치하기 위한 설정
    /// </summary>
    public void DirectSetUp()
    {
        toolPooling.toolInstallMode.DirectChange();
        toolPooling.indexToChange = index;
        CloseUI();
    }

    /// <summary>
    /// 풀링도 돌려주고, FoodData Amount와 ItemData Amount도 바꿔줌
    /// </summary>
    public void DeleteTool() 
    {
        WhenReturn();
        InstallData.Instance.DeleteData(index, InstallData.SortOfInstall.Tool);
        toolPooling.toolInstallMode._cookingCharacter.isToolCollider = false;
        toolPooling.toolInstallMode.PositionCollider[index].SetActive(true);
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
    
    IEnumerator CookingGauge() //LoadingBar.fillAmount이 1이 될때까지 점점 게이지를 추가해줌
    {
        while (blackCircle.fillAmount < 1)
        {
            currentValue += Time.deltaTime;
            blackCircle.fillAmount = currentValue / FoodInfos.MakeTime * GreenPotionEffect;
            blackCircleBig.fillAmount = currentValue / FoodInfos.MakeTime * GreenPotionEffect;
            circleUI.transform.Rotate(0, 0, 1);
            circleUIBig.transform.Rotate(0, 0, 1);
            yield return null;
        }
        soundManager.StopEffect3D(gameObject);
        isBeforeCooking = true;
        currentValue = 0;
        isCooked = true;
        food.sprite = ImageData.Instance.FindImageData(FoodInfos.ImageID);
        foodBig.sprite = ImageData.Instance.FindImageData(FoodInfos.ImageID);
        if (FoodInfos.ID != 100000)
        {
            StartCoroutine(_burntCoroutine);
        }
    }

    IEnumerator BurntFood()
    {
        while (blackCircle.fillAmount > 0)
        {
            //Debug.Log(currentValue / FoodInfos.MakeTime * 1.25f);
            currentValue += Time.deltaTime;
            blackCircle.fillAmount = 1 - (currentValue / (FoodInfos.MakeTime * 1.25f));
            blackCircleBig.fillAmount = 1 - (currentValue / (FoodInfos.MakeTime * 1.25f));
            yield return null;
        }
        currentValue = 0;
        food.sprite = ImageData.Instance.FindImageData(FoodData.Instance.foodTool[6].foodInfos[1].ImageID);
        foodBig.sprite = ImageData.Instance.FindImageData(FoodData.Instance.foodTool[6].foodInfos[1].ImageID); 
        FoodInfos = FoodData.Instance.foodTool[6].foodInfos[1];       

    }


}
