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
    private int _availableInven = 3;//??Í∞íÏù¥ Î∞îÎ?åÎ©¥ ?∏Î≤§?†Î¶¨ ?†Í∏à???¥Ï†ú?†Í±∞?àÍπê Ï¥àÍ∏∞?îÎèÑ Í≤åÏûÑ?∞Ïù¥?∞Ïóê???òÎ©¥ Ï¢ãÏùÑ??
    [HideInInspector]public CookingCharacter _cookingCharacter;

    public int AvailableInven
    {
        get { return _availableInven;}
        set
        {
            _availableInven = value;
        }
    }
    private int _maxInven = 6;//???òÏóê ?∞Îùº??UI Î∞îÎ?åÎäîÍ±??òÏ§ë??
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
            for (int i = 0; i < GameData.Instance.RainbowDrinking / 5; i++)//?¨Îü¨Î≤??ïÏû•?¥Ïïº ????
            {
                ExtensionInventory();
            }
            Debug.Log("?ïÏû•");
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
            //Debug.Log(i+"Î≤àÏß∏ ?¨Î°Ø ÏßÑÏûÖ");
            if (isUsed[i] == false)
            {
                //Debug.Log(i+"Î≤àÏß∏ ?¨Î°Ø??ÎπÑÏñ¥?àÏùå");
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
                //Debug.Log(i+"Î≤àÏß∏ ?¨Î°Ø??ÎπÑÏñ¥?àÏùå");
                EdibleItems[i]._itemType = EdibleItem.ItemType.Food;
                EdibleItems[i]._ingredientsInfos = null;
                EdibleItems[i]._foodInfos = food;
                chefSlotManager.AddFoodItem(food, i);
                isUsed[i] = true;
                if (!_cookingCharacter.isHand)//?åÏãù???§Ïñ¥???åÎßà???§Ìñâ?òÏ? ?äÍ∏∞ ?ÑÌï¥??
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
                    Debug.Log("¿ΩΩƒ ¿÷¿Ω");
                    return true;
                }
            }
        }
        Debug.Log("¿ΩΩƒ æ¯¿Ω");
        return false;
    }


    public void SendItem(int i)
    {
        if (isUsed[i])
        {
            if (_cookingCharacter.isSpace)//?∞ÌîÑ?∏Î≤§?†Î¶¨?êÏÑú Î≥¥ÎÇ¥???ëÏóÖ?? ?ÑÎ? ?§Ìéò?¥Ïä§Î∞îÎ°ú UIÎ•??¨Îäî Í≤ÉÏóê???úÏûë?òÍ∏∞ ?åÎ¨∏
            {
                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Ingredient)
                {
                    if (_cookingCharacter.isToolCollider) //?§Ìéò?¥Ïä§Î∞îÎ? ?âÏû•Í≥†Ïïà?¥ÎÇò?êÏÑú ?ÑÎ•∏Í±??ÑÎãåÏß? ?ïÏù∏ => ?§ÏúÑÏπòÎ°ú Î≥?Í≤?
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
                        Debug.Log("?åÏãù ?ÑÎã¨ ?ÑÎ£å");
                        //Í≤åÏä§?∏Í? ?àÎ∞õ?? ?ÅÌÉú?ºÎ©¥
                        if (_cookingCharacter._foodOrder.ReceiveFood(EdibleItems[i]._foodInfos.ID))
                        {
                            _cookingCharacter.uiMovement.foodOrderImage.sprite = ImageData.Instance.FindImageData(EdibleItems[i]._foodInfos.ImageID);
                            ChangeInventoryEmpty(i);
                            _cookingCharacter.uiMovement.CloseUI();
                            EdibleItems[i]._foodInfos = null;
                            if (!IsFoodInHand())//?∏Î≤§?†Î¶¨???åÏãù???ÑÏòà ?ÜÎã§Î©?
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
                           
                            SoundManager.Instance.Play(SoundManager.Instance._audioClips["PanIn"]);
                            ChangeInventoryEmpty(i);
                            if (!IsFoodInHand())
                            {
                                Debug.Log("æ≤∑π±‚≈Îø° πˆ∏≤");
                                _cookingCharacter.HoldDish(false);
                            }
                            return;
                        }
                    }
                }
            }
            
            else
            {
                
                //?îÎ¶¨ ?ÑÎã¨
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
