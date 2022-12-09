using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapemanShopNPC : ShopNPC
{
    public Transform[] SpawnPosition;
    private void Awake()
    {
        SetPositon();
    }

    protected override void ShopState(bool state)
    {
        base.ShopState(state);
        if(!state)
        {
            SetPositon();
        }
    }
    private void SetPositon()
    {
        int index = Random.Range(0, SpawnPosition.Length);
        Transform Spawn = SpawnPosition[index];
        this.gameObject.transform.position = Spawn.position;
        this.gameObject.transform.rotation = Spawn.rotation;
        Debug.Log(index+"제티 위치"+Spawn);
    }
    
}