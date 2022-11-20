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
    private int _availableInven = 2;//이 값이 바뀌면 인벤토리 잠금을 해제할거니깐 초기화도 게임데이터에서 하면 좋을듯
    [HideInInspector]public CookingCharacter _cookingCharacter;

    public int AvailableInven
    {
        get { return _availableInven;}
        set
        {
            _availableInven = value;
        }
    }
    private int _maxInven = 6;//이 수에 따라서 UI 바뀌는건 나중에
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


        //= new List<EdibleItem>()

    private void Start()
    {
        for (int i = 0; i < AvailableInven; i++)
        {
            EdibleItems[i]._itemType = EdibleItem.ItemType.Ingredient;
        }
        _cookingCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<CookingCharacter>();
    }
    public void ExtensionInventory()
    {
        if (_availableInven != _maxInven)
        {
            _availableInven++;
        }
        chefSlotManager.itemslots[_availableInven - 1].changeSlotUI(chefSlotManager.emptySlot);
        chefSlotManager.itemslots[_availableInven - 1].GetComponent<Button>().interactable = true;

    }

    public bool AddIngredient(ItemInfos ingredient)
    {
        for (int i = 0; i < AvailableInven; i++)
        {
            //Debug.Log(i+"번째 슬롯 진입");
            if (isUsed[i] == false)
            {
                //Debug.Log(i+"번째 슬롯이 비어있음");
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
                //Debug.Log(i+"번째 슬롯이 비어있음");
                EdibleItems[i]._itemType = EdibleItem.ItemType.Food;
                EdibleItems[i]._ingredientsInfos = null;
                EdibleItems[i]._foodInfos = food;
                chefSlotManager.AddFoodItem(food, i);
                isUsed[i] = true;
                if (!_cookingCharacter.isHand)//음식이 들어올 때마다 실행하지 않기 위해서
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
            if (_cookingCharacter.isSpace)//셰프인벤토리에서 보내는 작업은 전부 스페이스바로 UI를 여는 것에서 시작하기 때문
            {
                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Ingredient)
                {
                    if (_cookingCharacter.isToolCollider) //스페이스바를 냉장고안이나에서 누른건 아닌지 확인 => 스위치로 변경
                    {
                        if (_cookingCharacter._cookingTool.PutIngredient(EdibleItems[i]._ingredientsInfos.ID,
                            ImageData.Instance.FindImageData(EdibleItems[i]._ingredientsInfos.ImageID)) && EdibleItems[i]._ingredientsInfos.ID != 63)
                        {
                            
                            SoundManager.Instance.Play(SoundManager.Instance._audioClips["PanIn"]);
                            ChangeInventoryEmpty(i);
                            return;
                        };
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
                            ToolPooling.Instance.toolInstallMode.GetAndPosition(
                                _cookingCharacter._cookPosition.index, "Plate");
                            
                            _cookingCharacter._cookPosition.CloseUI(0);
                            ToolPooling.Instance.pooledObject[_cookingCharacter._cookPosition.index]
                                .InventoryBig.SetActive(true);
                            ChangeInventoryEmpty(i);
                            _cookingCharacter.isCookPositionCollider = false;
                            return;
                        }
                    }

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

                if (EdibleItems[i]._itemType == EdibleItem.ItemType.Food)
                {
                    if (_cookingCharacter.isGuestCollider)
                    {
                        Debug.Log("음식 전달 완료");
                        //게스트가 안받은 상태라면
                        if (_cookingCharacter._foodOrder.ReceiveFood(EdibleItems[i]._foodInfos.ID))
                        {
                            _cookingCharacter.uiMovement.foodOrderImage.sprite = ImageData.Instance.FindImageData(EdibleItems[i]._foodInfos.ImageID);
                            ChangeInventoryEmpty(i);
                            _cookingCharacter.uiMovement.CloseUI();
                            EdibleItems[i]._foodInfos = null;
                            if (!IsFoodInHand())//인벤토리에 음식이 아예 없다면
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
                            return;
                        }
                    }
                }
            }
            
            else
            {
                
                //요리 전달
            }
        }
        
        
        
    }

    private void ChangeInventoryEmpty(int index)
    {
        EdibleItems[index]._ingredientsInfos = null;
        EdibleItems[index]._foodInfos = null;
        isUsed[index] = false;
        chefSlotManager.itemslots[index].changeSlotUI(chefSlotManager.emptySlot);
    }
    
}
