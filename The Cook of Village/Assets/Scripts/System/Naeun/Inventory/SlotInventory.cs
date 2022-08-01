using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : Slot<InventoryItemInfos>
{
    public InventoryItemInfos ItemInfos
    {
        get { return Infos; }
        set 
        { 
            Infos = value;
            ModifySlot();
        }
    }
    [SerializeField] private GameObject ItemExplanation;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Text ExplanationText;
    [SerializeField] private Button UseItemButton;

    private void OnDisable()
    {
        ResetSlot();
    }
    public void ResetSlot()
    {
        ItemImage.gameObject.SetActive(false);
        Infos = null;
    }
    public override void SelectSlot()
    {
        ItemExplanation.SetActive(true);
        ExplanationText.text = Infos.Explanation;
        UseItemButton.gameObject.SetActive(Infos.type == InventoryType.Type.Potion);
    }
    public override void ModifySlot()
    {
        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = Infos.ImageUI;
    }
}
