/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public MaterialData data;
    public GameObject SlotContent;
    public SlotShop[] slot;
    public enum Shop {FruitShop, VegetableShop, MeatShop}
    [SerializeField]
    public Shop shop;

    Dictionary<Shop, int> ShopDictionary = new Dictionary<Shop, int>();
    private void Awake()
    {
        slot = SlotContent.transform.GetComponentsInChildren<SlotShop>(true);
        ShopDictionary.Add(Shop.FruitShop, 1);
        ShopDictionary.Add(Shop.VegetableShop, 2);
        ShopDictionary.Add(Shop.MeatShop, 3);
    }
    public void OpenShop()
    {
        this.gameObject.SetActive(true);
        SlotDataLoad();
    }
    public void CloseShop()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SlotDataLoad();
    }

    private void SlotDataLoad()
    {
        int order = 0;
        int type = ShopDictionary[shop];
        foreach (SlotShop slot in slot)
        {
            if(order < data.material[type].materialInfos.Count)
            {
                slot.Type = type;
                slot.ID = data.material[type].materialInfos[order].ID;
                slot.SlotImageData = data.material[type].materialInfos[order].ImageUI;
                slot.SlotCount = data.material[type].materialInfos[order].Amount;
                slot.Price = data.material[type].materialInfos[order].Price;
                order++;
            }
        }
    }
}
