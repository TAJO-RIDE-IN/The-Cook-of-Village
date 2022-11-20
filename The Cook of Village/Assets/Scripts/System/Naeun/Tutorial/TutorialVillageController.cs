using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVillageController : MonoBehaviour
{
    public TutorialUI VillageTutorialUI;
    public ThirdPersonGravity Player;
    public GameObject DestinationParticle;
    public List<GameObject> RoadArrowParticle = new List<GameObject>();
    public List<GameObject> Wall = new List<GameObject>();
    private int ActionNum;
    public void Start()
    {
        if(GameManager.Instance.gameMode == GameManager.GameMode.Tutorial)
        {
            Init();
        }
    }

    private void Init()
    {
        Player.StopMoving();
        VillageTutorialUI.DialogueState(true);
        VillageTutorialUI.CallDialogue("Control");
        foreach(var obj in Wall)
        {
            obj.SetActive(true);
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
                DestinationParticle.SetActive(true);
                break;
            case 1: //상점 도착
                ArrowParticleState(false, 0);
                Player.StopMoving();
                DestinationParticle.SetActive(false);
                break;
            case 2: //레스토랑으로 이동
                ArrowParticleState(true, 180);
                DestinationParticle.SetActive(false);
                break;
        }
        ActionNum++;
    }
}
