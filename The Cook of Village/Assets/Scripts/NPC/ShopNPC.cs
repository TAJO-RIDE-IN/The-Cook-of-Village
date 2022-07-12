using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour, IObserver<GameData>
{
    public ShopUI shopUI;
    public ShopUI.Shop type;
    private Animator ani;

    public int OpenTime;
    public int CloseTime;
    public int CloseDay;

    private void Start()
    {
        ani = this.gameObject.GetComponent<Animator>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            currentShop();
        }
    }

    private bool TodayShopOpen(float time ,bool open)
    {
        if(time >= OpenTime && time <= CloseTime)
        {
            if(open) return true;
            return false;
        }
        else
        {
            return false;
        }
    }

    private void ShopState(bool state)
    {
        this.gameObject.SetActive(state);
        this.gameObject.GetComponent<Collider>().enabled = state;
    }
    private void currentShop()
    {
        shopUI.shop = type;
    }    

    #region observer
    public void AddObserver(IGameDataOb o)
    {
        GameData.Instance.AddObserver(this);
    }

    public void RemoveObserver(IGameDataOb o)
    {
        GameData.Instance.RemoveObserver(this);
    }

    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            bool open = (GameData.Today == CloseDay) ? false : true;
            ShopState(TodayShopOpen(obj.TimeOfDay, open));
        }
    }
    #endregion
}
