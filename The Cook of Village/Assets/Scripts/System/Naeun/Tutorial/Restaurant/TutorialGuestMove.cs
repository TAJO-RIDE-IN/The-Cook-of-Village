using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuestMove : GuestMove
{
    public TutorialNPCController tutorialNPCController;
    protected override void OutChair()
    {
        tutorialNPCController.tutorialController.NextDialogue();
    }
    protected override void NPCState(string destination_name)
    {
        switch (destination_name)
        {
            case "Chair":
                transform.position = Sit.position;
                NPCLook(guest.chairUse.TablePosition.position);
                guest.ChangeState(GuestNPC.State.Sit);
                break;
            case "Door":
                guest.ChangeState(GuestNPC.State.GoOut);
                tutorialNPCController.tutorialController.EndEvent();
                break;
            case "Counter":
                guest.ChangeState(GuestNPC.State.Idle);
                break;
            case "CounterLine":
                guest.ChangeState(GuestNPC.State.Idle);
                break;
        }
    }
    protected override void ChairUse() //사용하지 않은 의자의 위치을 랜덤으로 출력, 사용중인 의자 저장
    {
        guest.chairUse = tutorialNPCController.Chair;
        Sit = guest.chairUse.SitPosition(guest.currentNPC);
    }
}
