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
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private GameObject ItemExplanation;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Text ItemCount;
    [SerializeField] private Text ExplanationText; //������ ����
    [SerializeField] private Button UseItemButton; //������ ��� ��ư

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
    public override void ModifySlot() //���� ���� ����
    {
        ObjectState(true);
        ItemCount.text = Infos.Amount.ToString();
        ItemImage.sprite = Infos.ImageUI;
    }
    public override void SelectSlot() //���� Ŭ��
    {
        UseItemButton.onClick.RemoveAllListeners();
        UseItemButton.onClick.AddListener(UseItem);
        UseItemButton.gameObject.SetActive(Infos.type == ItemType.Type.Potion);
        ItemExplanation.SetActive(true);
        ExplanationText.text = Infos.Explanation;
    }
    public void UseItem() //������ ���
    {
        Infos.Amount--;
        ItemData.Instance.ChangeAmount(Infos.ID, Infos.Amount);
        ItemCount.text = Infos.Amount.ToString();
        if (Infos.Amount <= 0)
        {
            inventoryUI.LoadInventorySlot(); //�κ��丮 ������
        }

/*        switch(Infos.type) //������ Ÿ�Կ� ���� ȣ�� �Լ� �޶���
        {
            case (ItemType.Type.Potion):
                break;
            case (ItemType.Type.CookingTool):
                break;
            case (ItemType.Type.Furniture):
                break;
        }*/
    }
}
