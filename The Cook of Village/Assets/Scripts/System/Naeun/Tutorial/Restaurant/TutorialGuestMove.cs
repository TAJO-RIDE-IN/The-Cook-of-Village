using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuestMove : GuestMove
{
    public TutorialNPCController tutorialNPCController;
    private void OutChair()
    {

    }

    protected override void ChairUse() //사용하지 않은 의자의 위치을 랜덤으로 출력, 사용중인 의자 저장
    {
        guest.chairUse = tutorialNPCController.Chair;
        Sit = guest.chairUse.SitPosition(guest.currentNPC);
    }
}
