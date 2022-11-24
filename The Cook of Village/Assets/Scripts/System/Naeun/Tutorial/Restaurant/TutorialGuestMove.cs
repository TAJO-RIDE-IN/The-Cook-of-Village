using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuestMove : GuestMove
{
    public TutorialNPCController chairContainer;
    private void Awake()
    {
        chairContainer = this.gameObject.transform.parent.GetComponent<TutorialNPCController>();
    }
}
