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
    private int _availableInven = 3;//??๊ฐ์ด ๋ฐ๋?๋ฉด ?ธ๋ฒค? ๋ฆฌ ? ๊ธ???ด์ ? ๊ฑฐ?๊น ์ด๊ธฐ?๋ ๊ฒ์?ฐ์ด?ฐ์???๋ฉด ์ข์??
    [HideInInspector]public CookingCharacter _cookingCharacter;

    public int AvailableInven
    {
        get { return _availableInven;}
        set
        {
            _availableInven = value;
        }
    }
    private int _maxInven = 6;//???์ ?ฐ๋ผ??UI ๋ฐ๋?๋๊ฑ??์ค??
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
            for (int i = 0; i < GameData.Instance.RainbowDrinking / 5; i++)//?ฌ๋ฌ๋ฒ??์ฅ?ด์ผ ????
            {
                ExtensionInventory();
            }
            Debug.Log("?์ฅ");
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
            //Debug.Log(i+"๋ฒ์งธ ?ฌ๋กฏ ์ง์");
            if (isUsed[i] == false)
            {
                //Debug.Log(i+"๋ฒ์งธ ?ฌ๋กฏ??๋น์ด?์");
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
                //Debug.Log(i+"๋ฒ์งธ ?ฌ๋กฏ??๋น์ด?์");
                EdibleItems[i]._itemType = EdibleItem.ItemType.Food;
                EdibleItems[i]._ingredientsInfos = null;
                EdibleItems[i]._foodInfos = food;
                chefSlotManager.AddFoodItem(food, i);
                isUsed[i] = true;
                if (!_cookingCharacter.isHand)//?์???ค์ด???๋ง???คํ?์? ?๊ธฐ ?ํด??
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
            if (_cookingCharacter.isSpace)//?ฐํ?ธ๋ฒค? ๋ฆฌ?์ ๋ณด๋ด???์?? ?๋? ?คํ?ด์ค๋ฐ๋ก UI๋ฅ??ฌ๋ ๊ฒ์???์?๊ธฐ ?๋ฌธ
            {
                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Ingredient)
                {
                    if (_cookingCharacter.isToolCollider) //?คํ?ด์ค๋ฐ๋? ?์ฅ๊ณ ์?ด๋?์ ?๋ฅธ๊ฑ??๋์ง? ?์ธ => ?ค์์น๋ก ๋ณ?๊ฒ?
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
                        Debug.Log("?์ ?๋ฌ ?๋ฃ");
                        //๊ฒ์ค?ธ๊? ?๋ฐ?? ?ํ?ผ๋ฉด
                        if (_cookingCharacter._foodOrder.ReceiveFood(EdibleItems[i]._foodInfos.ID))
                        {
                            _cookingCharacter.uiMovement.foodOrderImage.sprite = ImageData.Instance.FindImageData(EdibleItems[i]._foodInfos.ImageID);
                            ChangeInventoryEmpty(i);
                            _cookingCharacter.uiMovement.CloseUI();
                            EdibleItems[i]._foodInfos = null;
                            if (!IsFoodInHand())//?ธ๋ฒค? ๋ฆฌ???์???์ ?๋ค๋ฉ?
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
                                Debug.Log("พฒทนฑโล๋ฟก น๖ธฒ");
                                _cookingCharacter.HoldDish(false);
                            }
                            return;
                        }
                    }
                }
            }
            
            else
            {
                
                //?๋ฆฌ ?๋ฌ
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
