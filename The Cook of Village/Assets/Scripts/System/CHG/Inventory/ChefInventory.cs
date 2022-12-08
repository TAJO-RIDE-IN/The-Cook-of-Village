using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChefInventory : MonoBehaviour
{
    
    private static ChefInventory instance = null;
    
    private void Awake()
    {
        for (int i = 0; i < AvailableInven; i++)
        {
            EdibleItems[i] = new EdibleItem()
                {_itemType = EdibleItem.ItemType.Ingredient, _ingredientsInfos = null, _foodInfos = null};
        }
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ChefInventory Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public FridgeUI fridgeUI;
    public ChefItemSlotManager chefSlotManager;
    private int _availableInven = 3;//??κ°μ΄ λ°λ?λ©΄ ?Έλ²€? λ¦¬ ? κΈ???΄μ ? κ±°?κΉ μ΄κΈ°?λ κ²μ?°μ΄?°μ???λ©΄ μ’μ??
    [HideInInspector]public CookingCharacter _cookingCharacter;

    public int AvailableInven
    {
        get { return _availableInven;}
        set
        {
            _availableInven = value;
        }
    }
    private int _maxInven = 6;//???μ ?°λΌ??UI λ°λ?λκ±??μ€??
    public int MaxInven
    {
        get { return _maxInven;}
        set
        {
            _maxInven = value;
        }
    }
    
    [Serializable]
    public class EdibleItem
    {
        [Serializable]
        public enum ItemType
        {
            Ingredient, Food
        }
        [SerializeField] public ItemType _itemType;
        [SerializeField] public ItemInfos _ingredientsInfos;
        [SerializeField] public FoodInfos _foodInfos;
    }


    [SerializeField] private EdibleItem[] EdibleItems = new EdibleItem[6];

    public bool[] isUsed = Enumerable.Repeat(false, 6).ToArray();

    

    private ToolPooling toolPooling;


        //= new List<EdibleItem>()

    private void Start()
    {
        for (int i = 0; i < AvailableInven; i++)
        {
            EdibleItems[i]._itemType = EdibleItem.ItemType.Ingredient;
        }
        _cookingCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<CookingCharacter>();
        toolPooling = ToolPooling.Instance;
        
        if (GameData.Instance.isExtension)
        {
            for (int i = 0; i < GameData.Instance.RainbowDrinking / 5; i++)//?¬λ¬λ²??μ₯?΄μΌ ????
            {
                ExtensionInventory();
            }
            Debug.Log("?μ₯");
            GameData.Instance.isExtension = false;
        }
    }
    public void ExtensionInventory()
    {
        if (_availableInven != _maxInven)
        {
            _availableInven++;
        }
        chefSlotManager.itemslots[_availableInven - 1].changeSlotUI(chefSlotManager.emptyInven);
        chefSlotManager.itemslots[_availableInven - 1].GetComponent<Button>().interactable = true;
        chefSlotManager.itemslots[_availableInven - 1].slotUI2.sprite = chefSlotManager.emptySlot;
    }

    public bool AddIngredient(ItemInfos ingredient)
    {
        for (int i = 0; i < AvailableInven; i++)
        {
            //Debug.Log(i+"λ²μ§Έ ?¬λ‘― μ§μ");
            if (isUsed[i] == false)
            {
                //Debug.Log(i+"λ²μ§Έ ?¬λ‘―??λΉμ΄?μ");
                EdibleItems[i]._itemType = EdibleItem.ItemType.Ingredient;
                EdibleItems[i]._ingredientsInfos = ingredient;
                EdibleItems[i]._foodInfos = null;
                chefSlotManager.AddIngredientItem(ingredient, i);
                isUsed[i] = true;
                return true;
            }
        }
        chefSlotManager.ShowWarning();
        return false;
        
    }
    public bool AddFood(FoodInfos food)
    {
        for (int i = 0; i < AvailableInven; i++)
        {
            if (isUsed[i] == false)
            {
                //Debug.Log(i+"λ²μ§Έ ?¬λ‘―??λΉμ΄?μ");
                EdibleItems[i]._itemType = EdibleItem.ItemType.Food;
                EdibleItems[i]._ingredientsInfos = null;
                EdibleItems[i]._foodInfos = food;
                chefSlotManager.AddFoodItem(food, i);
                isUsed[i] = true;
                if (!_cookingCharacter.isHand)//?μ???€μ΄???λ§???€ν?μ? ?κΈ° ?ν΄??
                {
                    _cookingCharacter.HoldDish(true);
                }
                return true;
            }
            //SlotManager.AddFoodItem(food);
        }
        chefSlotManager.ShowWarning();
        return false;
    }

    private bool IsFoodInHand()
    {
        for (int i = 0; i < AvailableInven; i++)
        {
            if (isUsed[i])
            {
                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Food)
                {
                    return true;
                }
            }
        }
        return false;
    }


    public void SendItem(int i)
    {
        if (isUsed[i])
        {
            if (_cookingCharacter.isSpace)//?°ν?Έλ²€? λ¦¬?μ λ³΄λ΄???μ?? ?λ? ?€ν?΄μ€λ°λ‘ UIλ₯??¬λ κ²μ???μ?κΈ° ?λ¬Έ
            {
                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Ingredient)
                {
                    if (_cookingCharacter.isToolCollider) //?€ν?΄μ€λ°λ? ?μ₯κ³ μ?΄λ?μ ?λ₯Έκ±??λμ§? ?μΈ => ?€μμΉλ‘ λ³?κ²?
                    {
                        if (EdibleItems[i]._ingredientsInfos.ID != 63)
                        {
                            if (_cookingCharacter._cookingTool.PutIngredient(EdibleItems[i]._ingredientsInfos.ID,
                                ImageData.Instance.FindImageData(EdibleItems[i]._ingredientsInfos.ImageID)) && EdibleItems[i]._ingredientsInfos.ID != 63)
                            {
                            
                                SoundManager.Instance.Play(SoundManager.Instance._audioClips["PanIn"]);
                                ChangeInventoryEmpty(i);
                                return;
                            };
                        }
                        
                    }

                    if (_cookingCharacter.isFridgeCollider)
                    {
                        fridgeUI.InputRefrigerator(EdibleItems[i]._ingredientsInfos.ID, 1);
                        ChangeInventoryEmpty(i);
                        return;
                    }
                    if (_cookingCharacter.isCookPositionCollider)
                    {
                        if (EdibleItems[i]._ingredientsInfos.ID == 63)
                        {
                            toolPooling.toolInstallMode.GetAndPosition(
                                _cookingCharacter._cookPosition.index, "Plate");
                            ChangeInventoryEmpty(i);
                            toolPooling.toolInstallMode.DirectUICloseSetting();
                            toolPooling.toolInstallMode.DirectUIOpenSetting();
                            return;
                        }
                    }
                    if (_cookingCharacter.isObjectCollider)
                    {
                        if (_cookingCharacter.objectName == "Trash")
                        {
                            if (_cookingCharacter.trash.AddIngredient(EdibleItems[i]._ingredientsInfos))
                            {
                                SoundManager.Instance.Play(SoundManager.Instance._audioClips["PanIn"]);
                                ChangeInventoryEmpty(i);
                                return;
                            }
                        }
                    }
                }

                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Food)
                {
                    if (_cookingCharacter.isGuestCollider)
                    {
                        Debug.Log("?μ ?λ¬ ?λ£");
                        //κ²μ€?Έκ? ?λ°?? ?ν?Όλ©΄
                        if (_cookingCharacter._foodOrder.ReceiveFood(EdibleItems[i]._foodInfos.ID))
                        {
                            _cookingCharacter.uiMovement.foodOrderImage.sprite = ImageData.Instance.FindImageData(EdibleItems[i]._foodInfos.ImageID);
                            ChangeInventoryEmpty(i);
                            _cookingCharacter.uiMovement.CloseUI();
                            EdibleItems[i]._foodInfos = null;
                            if (!IsFoodInHand())//?Έλ²€? λ¦¬???μ???μ ?λ€λ©?
                            {
                                _cookingCharacter.HoldDish(false);
                            }
                            return;
                        }
                    }
                    if (_cookingCharacter.objectName == "Trash")
                    {
                        if (_cookingCharacter.trash.AddFood(EdibleItems[i]._foodInfos))
                        {
                            if (!IsFoodInHand())
                            {
                                _cookingCharacter.HoldDish(false);
                            }
                            SoundManager.Instance.Play(SoundManager.Instance._audioClips["PanIn"]);
                            ChangeInventoryEmpty(i);
                            return;
                        }
                    }
                }
            }
            
            else
            {
                
                //?λ¦¬ ?λ¬
            }
        }
        
        
        
    }

    private void ChangeInventoryEmpty(int index)
    {
        EdibleItems[index]._ingredientsInfos = null;
        EdibleItems[index]._foodInfos = null;
        isUsed[index] = false;
        chefSlotManager.itemslots[index].changeSlotUI(chefSlotManager.emptyInven);
    }
    
}
