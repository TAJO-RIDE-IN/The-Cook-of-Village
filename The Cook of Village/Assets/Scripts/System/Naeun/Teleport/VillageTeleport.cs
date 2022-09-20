using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VillageTeleport : MonoBehaviour
{
    public Transform PotionShop;
    public Transform PotionShopOut;
    public GameObject InteriorShop;
    public GameObject InteriorShopOut;
    public GameObject Bank;
    public GameObject BankOut;
    public ParticleSystem MoveEffect;
    public GameObject Player;

    public Dictionary<Gate, Transform> GateDictionary = new Dictionary<Gate, Transform>();
    private void Awake()
    {
        GateDictionary.Add(Gate.PotionShop, PotionShopOut);
        GateDictionary.Add(Gate.PotionShopOut, PotionShop);
/*        GateDictionary.Add(Gate.InteriorShop, InteriorShopOut);
        GateDictionary.Add(Gate.InteriorShopOut, InteriorShop);
        GateDictionary.Add(Gate.Bank, BankOut);
        GateDictionary.Add(Gate.BankOut, Bank);*/
    }
    public enum Gate { PotionShop, PotionShopOut, InteriorShop, InteriorShopOut, Bank, BankOut};
    private void PlayMoveEffect()
    {
        MoveEffect.gameObject.SetActive(true);
        MoveEffect.Play();
    }

    public void MoveGate(Gate gate)
    {
        Vector3 TeleportPosition = new Vector3(GateDictionary[gate].position.x + 10, GateDictionary[gate].position.y, GateDictionary[gate].position.z);
        Debug.Log(TeleportPosition);
        Player.transform.position = TeleportPosition;
        PlayMoveEffect();
    }
}
