using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : Slot<ItemInfos>
{
    public ItemInfos ItemInfos
    {
        get { return Infos; }
        set 
        { 
            Infos = value;
            ModifySlot();
        }
    }
    public InventoryUI inventoryUI;
    [SerializeField] private GameObject ItemExplanation;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Text ItemCount;
    [SerializeField] private Text ExplanationText; //아이템 설명
    [SerializeField] private Button UseItemButton; //아이템 사용 버튼

    private void OnDisable()
    {
        ResetSlot();
    }
    private void ObjectState(bool state)
    {
        ItemImage.gameObject.SetActive(state);
        ItemCount.gameObject.transform.parent.gameObject.SetActive(state);
        this.GetComponent<Button>().enabled = state;
    }
    public void ResetSlot()
    {
        ObjectState(false);
        Infos = null;
    }
    public override void ModifySlot() //슬롯 정보 수정
    {
        ObjectState(true);
        ItemCount.text = Infos.Amount.ToString();
        ItemImage.sprite = Infos.ImageUI;
    }
    public bool UseButtonState()
    {
        if(Infos.type == ItemType.Type.Potion)
        {
            return true;
        }
        else if(Infos.type == ItemType.Type.CookingTool || Infos.type == ItemType.Type.Furniture)
        {
            return GameManager.Instance.CurrentSceneIndex == 3;
        }
        return false;
    }
    public override void SelectSlot() //슬롯 클릭
    {
        UseItemButton.onClick.RemoveAllListeners();
        UseItemButton.onClick.AddListener(UseItem); 
        UseItemButton.gameObject.SetActive(UseButtonState());
        ItemExplanation.SetActive(true);
        ExplanationText.text = Infos.Explanation;
    }
    public void UseItem() //아이템 사용
    {
        switch (Infos.type) //아이템 타입에 따라 호출 함수 달라짐
        {
            case (ItemType.Type.Potion):
                Potion.Instance.UsePotion(Infos.Name);          
                break;
            case (ItemType.Type.CookingTool):
                ToolPooling.Instance.toolInstallMode.Use(Infos);
                ToolPooling.Instance.SelectedToolID = Infos.ID;
                break;
            case (ItemType.Type.Furniture):
                FurniturePooling.Instance.furnitureInstallMode.UseFurniture(Infos.Name);
                break;
        }
        Infos.Amount--;
        ItemCount.text = Infos.Amount.ToString();
        if (Infos.Amount <= 0)
        {
            inventoryUI.LoadInventorySlot(); //인벤토리 재정렬
        }
        inventoryUI.InventoryState();
    }
}
