using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVillageController : MonoBehaviour
{
    public TutorialUI VillageTutorialUI;
    public ThirdPersonGravity Player;
    public GameObject DestinationParticle, RestaurantParticle;
    public List<GameObject> RoadArrowParticle = new List<GameObject>();
    public List<GameObject> Wall = new List<GameObject>();
    private GameManager gameManager;
    private NPCData npcData;
    private int ActionNum;
    public void Start()
    {
        if(GameManager.Instance.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            gameManager = GameManager.Instance;
            npcData = NPCData.Instance;
            Init();
            NPCDisable();
        }
    }
    private void Init()
    {
        Player.StopMoving();
        gameManager.TutorialUI = true;
        VillageTutorialUI.DialogueState(true);
        VillageTutorialUI.CallDialogue("Control");
        RestaurantParticle.SetActive(false);
        foreach (var obj in Wall)
        {
            obj.SetActive(true);
        }
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
                Player.StartMoving();
                gameManager.TutorialUI = false;
                DestinationParticle.SetActive(true);
                break;
            case 1: //상점 도착
                ArrowParticleState(false, 0);
                Player.StopMoving();
                DestinationParticle.SetActive(false);
                break;
            case 2: //레스토랑으로 이동
                ArrowParticleState(true, 180);
                Player.StartMoving();
                npcData.AtOnceCloseNPC((int)NPCInfos.Work.FruitShop); 
                DestinationParticle.SetActive(false);
                RestaurantParticle.SetActive(true);
                break;
        }
        ActionNum++;
    }
}
