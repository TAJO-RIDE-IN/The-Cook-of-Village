using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuestMove : GuestMove
{
    public TutorialNPCController tutorialNPCController;
    private void OutChair()
    {

    }

    protected override void ChairUse() //������� ���� ������ ��ġ�� �������� ���, ������� ���� ����
    {
        guest.chairUse = tutorialNPCController.Chair;
        Sit = guest.chairUse.SitPosition(guest.currentNPC);
    }
}
