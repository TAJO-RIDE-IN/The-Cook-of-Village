using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotFridge : Slot<ItemInfos>
{
    public Button slot;
    public Text CountText;
    public Text IngredientName;
    public Image IngredientImage;
    public Image BasketImage;
    public Sprite NoneBasketImage;
    public Sprite UseBasketImage;
    public ItemInfos itemInfos
    {
        get { return Infos; }
        set
        {
            Infos = value;
            ModifySlot();
        }
    }
    /// <summary>
    /// 재료 개수가 0 이상인 경우와 0개인 경우의 모습을 다르게 함.
    /// </summary>
    private void SlotState()
    {
        BasketImage.sprite = (Infos.Amount > 0) ? UseBasketImage : NoneBasketImage;
        IngredientImage.gameObject.SetActive(Infos.Amount > 0);
        slot.interactable = (Infos.Amount > 0);
    }
    public override void ModifySlot()
    {
        CountText.text = "X" + Infos.Amount;
        IngredientName.text = Infos.KoreanName;
        IngredientImage.sprite = Infos.ImageUI;
        SlotState();
    }
    public override void SelectSlot()
    {
        if (Infos.Amount > 0)
        {
            if (ChefInventory.Instance.AddIngredient(Infos))
            {
                Infos.Amount--;
                ModifySlot();
            }
        }
    }
}
