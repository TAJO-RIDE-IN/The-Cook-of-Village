using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VillageTeleport : MonoBehaviour
{
    public static TeleportGate PotionShop;
    public static TeleportGate PotionShopOut;
    public static TeleportGate InteriorShop;
    public static TeleportGate InteriorShopOut;
    public static TeleportGate Bank;
    public static TeleportGate BankOut;
    public ParticleSystem MoveEffect;
    public GameObject Player;

    public Dictionary<TeleportGate, TeleportGate> GateDictionary = new Dictionary<TeleportGate, TeleportGate>
    {
        { PotionShop, PotionShopOut}, {PotionShopOut, PotionShop},
        {InteriorShop, InteriorShopOut}, {InteriorShopOut, InteriorShop},
        {Bank, BankOut}, {BankOut, Bank}
    };
    private void PlayMoveEffect()
    {
        MoveEffect.Play();
    }

    public void MoveGate(TeleportGate gate)
    {
        Player.transform.position = GateDictionary[gate].gameObject.transform.position;
        PlayMoveEffect();
    }
}
