using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodModel
{
    public string FoodName;
    public int FoodID;
    public GameObject FoodObject;
}
[System.Serializable]
public class VillageSitPosition
{
    public string Name;
    public int WorkID;
    public Transform SitPosition;
}
public class ChairUse : MonoBehaviour
{
    public VillageSitPosition[] VillageSitPosition;
    public FoodModel[] FoodModel;
    public Transform[] Destination;
    public Transform TablePosition;
    public Transform GuestSitPosition;
    public ParticleSystem FoodEffect;
    public GameObject Model(int id)
    {
        foreach(var obj in FoodModel)
        {
            if(obj.FoodID == id)
            {
                return obj.FoodObject;
            }
        }
        return null;
    }
    public Transform CloseDestination(Transform pos)
    {
        Vector3 distance1 = pos.position - Destination[0].position;
        Vector3 distance2 = pos.position - Destination[1].position;
        float fDist1 = distance1.sqrMagnitude;
        float fDist2 = distance2.sqrMagnitude;
        Transform destination = (fDist1 < fDist2) ? Destination[0]: Destination[1];
        return destination;
    }
    public void FoodEnable(int id, bool state)
    {
        Model(id).SetActive(state);
        if(state)
        {
            FoodEffect.Play();
        }
    }
    /// <summary>
    /// 주민마다 앉는 위치를 찾는다.
    /// </summary>
    /// <param name="npc">NPC가 마을 주민인지 기본 주민인지 입력</param>
    /// <param name="work">마을 주민이라면 workID 입력</param>
    /// <returns>앉는 위치 Transform 리턴</returns>
    public Transform SitPosition(GuestNPC.Guest npc, int work = 0)
    {
        if(npc == GuestNPC.Guest.Villge)
        {
            foreach (var obj in VillageSitPosition)
            {
                if (obj.WorkID == work)
                {
                    return obj.SitPosition;
                }
            }
        }
        return GuestSitPosition;
    }
}
