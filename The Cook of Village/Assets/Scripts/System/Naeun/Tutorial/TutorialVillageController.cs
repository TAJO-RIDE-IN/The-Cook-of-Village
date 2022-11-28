using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVillageController : TutorialController
{
    public TutorialUI VillageTutorialUI;
    public ThirdPersonGravity Player;
    public GameObject DestinationParticle, RestaurantParticle;
    public Collider shopCllider;
    public TutorialDetailsUI TutorialShop;
    public GameObject Wall;
    public List<GameObject> RoadArrowParticle = new List<GameObject>();
    private NPCData npcData;
    private int ActionNum;
    public override void Init()
    {
        npcData = NPCData.Instance;
        NPCDisable();
        Player.StopWalking();
        gameManager.TutorialUI = true;
        VillageTutorialUI.enabled = true;
        VillageTutorialUI.DialogueState(true);
        VillageTutorialUI.CallDialogue("Control");
        RestaurantParticle.SetActive(false);
        shopCllider.enabled = false;
        TutorialShop.enabled = true;
        Wall.SetActive(true);

    }
    private void NPCDisable()
    {
        foreach(var npc in npcData.npcInfos)
        {
            if(!npc.work.Equals(NPCInfos.Work.FruitShop))
            {
                npcData.AtOnceCloseNPC(npc.work);
            }
        }
    }
    public void ArrowParticleState(bool state,int direction)
    {
        foreach(var arrow in RoadArrowParticle)
        {
            arrow.SetActive(state);
            arrow.transform.eulerAngles = new Vector3(90, 0, direction);
        }
    }
    public void PurchaseAction()
    {
        switch (ActionNum)
        {
            case 0: //상점으로 이동
                ArrowParticleState(true, 0);
                Player.StartWalking();
                gameManager.TutorialUI = false;
                DestinationParticle.SetActive(true);
                break;
            case 1: //상점 도착
                ArrowParticleState(false, 0);
                shopCllider.enabled = true;
                Player.StopWalking();
                DestinationParticle.SetActive(false);
                break;
            case 2: //레스토랑으로 이동
                ArrowParticleState(true, 180);
                Player.StartWalking();
                npcData.AtOnceCloseNPC((int)NPCInfos.Work.FruitShop); 
                DestinationParticle.SetActive(false);
                RestaurantParticle.SetActive(true);
                break;
        }
        ActionNum++;
    }
    public override void PlayAction(bool state)
    {
        if (state)
        {
            PurchaseAction();
        }
        else
        {
            Player.StopWalking();
        }
    }
    public override void NextDialogue()
    {
        VillageTutorialUI.DialogueText();
    }
}
