using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : MonoBehaviour
{
    public InventoryItemInfos ItemInfos;
    [SerializeField] private GameObject ItemExplanation;
    [SerializeField] private Text ExplanationText;
    [SerializeField] private Button UseItemButton;

    public void ClickSlot()
    {
        ItemExplanation.SetActive(true);
        ExplanationText.text = ItemInfos.Explanation;
        UseItemButton.gameObject.SetActive(ItemInfos.type == InventoryType.Type.Potion);
    }
}
