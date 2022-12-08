using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapemanShopNPC : ShopNPC
{
    public Transform[] SpawnPosition;
    public Transform CapemanPosition()
    {
        return SpawnPosition[Random.Range(0, SpawnPosition.Length)];
    }
    protected override void ShopState(bool state)
    {
        base.ShopState(state);
        if(!state)
        {
            this.gameObject.transform.position = CapemanPosition().position;
            this.gameObject.transform.rotation = CapemanPosition().rotation;
        }
    }
    
}