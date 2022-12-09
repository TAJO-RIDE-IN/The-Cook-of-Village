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
    private int _availableInven = 3;//??ê°ì´ ë°ë?ë©´ ?¸ë²¤? ë¦¬ ? ê¸???´ì ? ê±°?ê¹ ì´ê¸°?ë ê²ì?°ì´?°ì???ë©´ ì¢ì??
    [HideInInspector]public CookingCharacter _cookingCharacter;

    public int AvailableInven
    {
        get { return _availableInven;}
        set
        {
            _availableInven = value;
        }
    }
    private int _maxInven = 6;//???ì ?°ë¼??UI ë°ë?ëê±??ì¤??
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
            for (int i = 0; i < GameData.Instance.RainbowDrinking / 5; i++)//?¬ë¬ë²??ì¥?´ì¼ ????
            {
                ExtensionInventory();
            }
            Debug.Log("?ì¥");
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
            //Debug.Log(i+"ë²ì§¸ ?¬ë¡¯ ì§ì");
            if (isUsed[i] == false)
            {
                //Debug.Log(i+"ë²ì§¸ ?¬ë¡¯??ë¹ì´?ì");
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
                //Debug.Log(i+"ë²ì§¸ ?¬ë¡¯??ë¹ì´?ì");
                EdibleItems[i]._itemType = EdibleItem.ItemType.Food;
                EdibleItems[i]._ingredientsInfos = null;
                EdibleItems[i]._foodInfos = food;
                chefSlotManager.AddFoodItem(food, i);
                isUsed[i] = true;
                if (!_cookingCharacter.isHand)//?ì???¤ì´???ë§???¤í?ì? ?ê¸° ?í´??
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
                    Debug.Log("À½½Ä ÀÖÀ½");
                    return true;
                }
            }
        }
        Debug.Log("À½½Ä ¾øÀ½");
        return false;
    }


    public void SendItem(int i)
    {
        if (isUsed[i])
        {
            if (_cookingCharacter.isSpace)//?°í?¸ë²¤? ë¦¬?ì ë³´ë´???ì?? ?ë? ?¤í?´ì¤ë°ë¡ UIë¥??¬ë ê²ì???ì?ê¸° ?ë¬¸
            {
                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Ingredient)
                {
                    if (_cookingCharacter.isToolCollider) //?¤í?´ì¤ë°ë? ?ì¥ê³ ì?´ë?ì ?ë¥¸ê±??ëì§? ?ì¸ => ?¤ìì¹ë¡ ë³?ê²?
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
                        Debug.Log("?ì ?ë¬ ?ë£");
                        //ê²ì¤?¸ê? ?ë°?? ?í?¼ë©´
                        if (_cookingCharacter._foodOrder.ReceiveFood(EdibleItems[i]._foodInfos.ID))
                        {
                            _cookingCharacter.uiMovement.foodOrderImage.sprite = ImageData.Instance.FindImageData(EdibleItems[i]._foodInfos.ImageID);
                            ChangeInventoryEmpty(i);
                            _cookingCharacter.uiMovement.CloseUI();
                            EdibleItems[i]._foodInfos = null;
                            if (!IsFoodInHand())//?¸ë²¤? ë¦¬???ì???ì ?ë¤ë©?
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
                                Debug.Log("¾²·¹±âÅë¿¡ ¹ö¸²");
                                _cookingCharacter.HoldDish(false);
                            }
                            return;
                        }
                    }
                }
            }
            
            else
            {
                
                //?ë¦¬ ?ë¬
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
