/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
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
        GameManager.Instance.IsUI = true;
        this.gameObject.SetActive(true);
        SlotDataLoad();
    }
    public void CloseShop()
    {
        GameManager.Instance.IsUI = false;
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
            if(order < MaterialData.Instance.materialType[type].materialInfos.Count) // Slot이 재료보다 많을 경우 대비
            {
                slot.materialInfos = MaterialData.Instance.materialType[type].materialInfos[order];
                slot.gameObject.SetActive(true);
                order++;
            }
        }
    }
}
