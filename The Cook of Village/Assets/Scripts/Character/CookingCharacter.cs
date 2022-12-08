using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CookingCharacter : MonoBehaviour
{
    public Animator charAnimator;
    public GameObject HandPosition;
    public GameObject TrashUI;
    


    public CalendarUI calendarUI;
    public Trash trash;
    [HideInInspector] public CookingTool _cookingTool;
    [HideInInspector] public FoodOrder _foodOrder;
    [HideInInspector] public CookPosition _cookPosition;
    private Fridge fridge;
    private GameData _gameData;
    private AnimatorOverrideController animatorOverrideController;
    private SoundManager soundManager;
    private ThirdPersonMovement _thirdPersonMovement;
    public AnimationClip[] Idle;
    public AnimationClip[] Walk;
    public UIMovement uiMovement;
    public GameObject DishObject;

    public bool isToolCollider;
    public bool isGuestCollider;
    public bool isFridgeCollider;
    public bool isCookPositionCollider;
    public bool isObjectCollider;
    public bool isHand;
    public bool isSpace;
    public GameObject box;
    public Text text;

    private bool isDestroy;
    private GameManager _gameManager;

    //
    [HideInInspector] public string objectName;


    void Start()
    {
        _thirdPersonMovement = GameObject.FindWithTag("Player").GetComponent<ThirdPersonMovement>();
        isSpace = false;
        fridge = GameObject.FindGameObjectWithTag("Fridge").GetComponent<Fridge>();
        animatorOverrideController = new AnimatorOverrideController(charAnimator.runtimeAnimatorController);
        charAnimator.runtimeAnimatorController = animatorOverrideController;
        _gameManager = GameManager.Instance;
        _gameData = GameData.Instance;
        
    }

    private void Update()
    {
        if (isObjectCollider || isToolCollider || isGuestCollider || isFridgeCollider || isCookPositionCollider)
        {
            WhenKeyDown();
        }
    }

    public void HoldDish(bool isTrue)
    {
        if (isTrue)
        {
            isHand = true;
            animatorOverrideController["Idle"] = Idle[1];
            animatorOverrideController["Walking"] = Walk[1];
            DishObject.SetActive(true);
        }
        else
        {
            isHand = false;
            animatorOverrideController["Idle"] = Idle[0];
            animatorOverrideController["Walking"] = Walk[0];
            DishObject.SetActive(false);
        }
    }

    //isHand �?거짓 ?��? ?�이 ?�행?�어???�는 �?: ?�장�? ?�러가�? ?�력 보기

    private void WhenKeyDown()//?�래 ?�료?�는 ?�수?�?�데 ?�페?�스바누를때 ?�료�??�는�??�니�??�양?�걸 ?�는???�페?�스�??��???모든 ?�수 ?�행?�도�?최적?��? ?�해???�렇�??�기로함
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isToolCollider)//?�리?�구???�어갔을?�만,?�리중이 ?�닐?�만 ?�료?�는�??�행
            {
                if (isSpace)
                {
                    isSpace = false;

                    _cookingTool.CloseUI();
                    //_cookingTool.InventoryBig.SetActive(false);
                    return;
                }
                else
                {
                    isSpace = true;
                    _cookingTool.OpenUI(1.3f);
                    //_cookingTool.InventoryBig.SetActive(true);
                    return;
                }
            }
            if (isGuestCollider)
            {
                if (isSpace)
                {
                    if (!_foodOrder.CanReceive)
                    {
                        uiMovement.CloseUI();
                        isSpace = false;
                        //UI ?�우�?
                        return;
                    }
                }
                else
                {
                    if (_foodOrder.CanReceive)
                    {
                        uiMovement.ShowUI();
                        isSpace = true;
                        //UI ?�기
                        return;
                    }
                }
            }
            if (isFridgeCollider)
            {
                fridge.UseRefrigerator(!fridge.isUsing);
                isSpace = !isSpace;
                return;
            }
            if (isCookPositionCollider)
            {
                if (isSpace)
                {
                    _cookPosition.CloseUI(1);
                    isSpace = false;
                    return;
                }
                else
                {
                    _cookPosition.OpenUI(1);
                    isSpace = true;
                    return;
                }
            }

            if (isObjectCollider)
            {
                if (objectName.Equals("Bed"))
                {
                    GameData.Instance.SetTimeMorning();
                    //Debug.Log("?�침?�로 변�?);
                    return;
                }

                if (objectName.Equals("Flour"))
                {
                    if (ChefInventory.Instance.AddIngredient(ItemData.Instance.ItemType[0]
                        .ItemInfos[0]))
                    {
                        return;
                        //?�반??밀가�??�성
                    }
                }
                if (objectName.Equals("Sugar"))
                {
                    if (ChefInventory.Instance.AddIngredient(ItemData.Instance.ItemType[0]
                        .ItemInfos[1]))
                    {
                        return;
                        //?�반??밀가�??�성
                    }
                }
                if (objectName.Equals("Cabinet"))
                {
                    if (ChefInventory.Instance.AddIngredient(ItemData.Instance.ItemType[6]
                        .ItemInfos[3]))
                    {
                        return;
                    }
                    else
                    {
                        ChefInventory.Instance.chefSlotManager.ShowWarning();
                        return;
                    }
                }
                if (objectName.Equals("Trash"))
                {
                    if (!isSpace) //?�는 ?�황
                    {
                        isSpace = true;
                        TrashUI.SetActive(true);
                        return;
                    }
                    else
                    {
                        isSpace = false;
                        TrashUI.SetActive(false);
                        return;
                    }
                }

                if (objectName == "Calendar")
                {
                    
                    calendarUI.CalendarUIState(!calendarUI.gameObject.activeSelf);
                    

                }

                if (objectName == "PROP_bed")
                {
                    if (_gameData.TimeOfDay > 1200 || _gameData.TimeOfDay < 120)
                    {
                        StartCoroutine(_thirdPersonMovement.WaitParticle());
                        _gameData.SetTimeMorning();
                    }
                    else
                    {
                        StartCoroutine(TextFade(box, text));
                    }
                    
                }
            }


        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("CookingTools"))
        {
            isToolCollider = true;
            _cookingTool = other.transform.GetComponent<CookingTool>();
            return;
        }


        if (other.CompareTag("Fridge"))
        {
            isFridgeCollider = true;
            isToolCollider = false;
            return;
        }
        if (other.CompareTag("CookPosition"))
        {
            isCookPositionCollider = true;
            _cookPosition = other.GetComponent<CookPosition>();
            return;
        }
        isObjectCollider = true;
        objectName = other.gameObject.name;
        //Debug.Log(other.gameObject.name + "??진입");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("CounterPosition"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                other.GetComponent<CounterQueue>().PayCounter();
            }
        }
        if (other.CompareTag("Guest"))
        {
            isGuestCollider = true;
            _foodOrder = other.GetComponent<FoodOrder>();
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fridge"))
        {
            if(fridge.isUsing)
            {
                fridge.UseRefrigerator(false);
            }
            isFridgeCollider = false;
            isSpace = false;
        }

        if (other.CompareTag("CookingTools"))
        {
            _cookingTool.InventoryBig.SetActive(false);
            isToolCollider = false;
            isSpace = false;
            _cookingTool.CloseUI();
        }
        if (other.CompareTag("Guest"))
        {
            uiMovement.CloseUI();
            isSpace = false;
            isGuestCollider = false;
        }

        if (other.CompareTag("CookPosition"))
        {
            _cookPosition.CloseUI(1);
            isCookPositionCollider = false;

        }
        if (other.gameObject.name.Equals("Trash"))
        {
            TrashUI.SetActive(false);
            isSpace = false;
            isObjectCollider = false;
        }
        if (other.gameObject.name.Equals("Calendar"))
        {
            calendarUI.CalendarUIState(false);
            isSpace = false;
            isObjectCollider = false;
        }
        
        else
        {
            isObjectCollider = false;
            isSpace = false;

        }
    }
    public IEnumerator TextFade(GameObject box, Text text)
    {
        box.SetActive(true);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        box.SetActive(false);
    }
    

}
