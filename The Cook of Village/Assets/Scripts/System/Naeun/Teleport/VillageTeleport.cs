using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VillageTeleport : MonoBehaviour
{
    public Transform PotionShop;
    public Transform PotionShopOut;
    public Transform InteriorShop;
    public Transform InteriorShopOut;
    public Transform Bank;
    public Transform BankOut;
    public ParticleSystem MoveEffect;
    public ThirdPersonGravity Player;

    public Dictionary<Gate, Transform> GateDictionary = new Dictionary<Gate, Transform>();

    private void Awake()
    {
        GateDictionary.Add(Gate.PotionShop, PotionShopOut);
        GateDictionary.Add(Gate.PotionShopOut, PotionShop);
        GateDictionary.Add(Gate.InteriorShop, InteriorShopOut);
        GateDictionary.Add(Gate.InteriorShopOut, InteriorShop);
        GateDictionary.Add(Gate.Bank, BankOut);
        GateDictionary.Add(Gate.BankOut, Bank);
    }
    public enum Gate { PotionShop, PotionShopOut, InteriorShop, InteriorShopOut, Bank, BankOut};
    private void PlayMove(Gate gate)
    {
        Player.transform.position = GateDictionary[gate].position;
        Player.transform.rotation = GateDictionary[gate].rotation;  
    }

    public void MoveGate(Gate gate)
    {
        MoveEffect.gameObject.SetActive(true); 
        MoveEffect.Play();
        Player.StopWalking();
        StartCoroutine(ChangeWithDelay.CheckDelay(0.5f, () => PlayMove(gate)));
        StartCoroutine(ChangeWithDelay.CheckDelay(1.5f, () => Player.StartWalking()));
    }
}
