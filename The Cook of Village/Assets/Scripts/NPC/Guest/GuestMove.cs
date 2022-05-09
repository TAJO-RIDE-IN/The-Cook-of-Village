using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GuestMove : GuestNPC, IObserver
{
    public GameObject Counter;
    public GameObject Door;
    private GameObject UseChair;
    private NPCPooling chairContainer;
    private NavMeshAgent agent;
    private bool isArrive = false;
    private void Awake()
    {
        chairContainer = this.gameObject.transform.parent.GetComponent<NPCPooling>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        StartCoroutine(NPCMove(ChairPosition()));
    }
    private void ChangeChairState()
    {
        chairContainer.WaitChair.Add(UseChair);
        UseChair = null;
    }

    private Vector3 ChairPosition() //사용하지 않은 의자의 위치을 랜덤으로 출력, 사용중인 의자 저장
    {
        int chairNum = Random.Range(0, chairContainer.WaitChair.Count);
        Vector3 destination = chairContainer.WaitChair[chairNum].transform.position;
        UseChair = chairContainer.WaitChair[chairNum];
        chairContainer.WaitChair.RemoveAt(chairNum);
        return (destination);
    }

    private IEnumerator NPCMove(Vector3 destination)
    {
        while (!isArrive)
        {
            agent.SetDestination(destination);
            if (agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 0.5f)
            {
                CurrentState = State.Sit;
                isArrive = true;
            }
            yield return null;
        }
    }


    public GuestMove(GuestNPC guestNPC)
    {
        guestNPC.AddObsever(this);
    }
    public void Change(Object obj)
    {
        if(obj is GuestNPC)
        {
            var guestNPC = obj as GuestNPC;
            Debug.Log("observer");
        }
    }
}
