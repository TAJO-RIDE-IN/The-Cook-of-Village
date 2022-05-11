using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GuestMove : MonoBehaviour, IObserver
{
    public Transform Counter;
    public Transform Door;
    private GameObject UseChair;
    private NPCPooling chairContainer;
    private NavMeshAgent agent;
    private GuestNPC guest;
    private bool isArrive = false;
    private void Awake()
    {
        chairContainer = this.gameObject.transform.parent.GetComponent<NPCPooling>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        guest = this.gameObject.GetComponent<GuestNPC>();
        guest.AddGuestNPC(new Guest());
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

    private Vector3 ChairPosition() //������� ���� ������ ��ġ�� �������� ���, ������� ���� ����
    {
        int chairNum = Random.Range(0, chairContainer.WaitChair.Count);
        Vector3 destination = chairContainer.WaitChair[chairNum].transform.position;
        UseChair = chairContainer.WaitChair[chairNum];
        chairContainer.WaitChair.RemoveAt(chairNum);
        return (destination);
    }

    private IEnumerator NPCMove(Vector3 destination)
    {
        if(guest.CurrentState != GuestNPC.State.Walk)
        {
            guest.ChangeState(GuestNPC.State.Walk);
        }
        while (!isArrive)
        {
            agent.SetDestination(destination);
            if (agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 0.5f)
            {
                isArrive = true;
            }
            yield return null;
        }
    }

    public void AddObserver() //MonoBehaviour ������ new ���Ұ�
    {
        guest.AddObserver(this);
    }

    public void Change(GuestNPC obj)
    {
        if(obj is GuestNPC)
        {
            var guestNPC = obj;
            if(guestNPC.CurrentState == GuestNPC.State.StandUP)
            {
                ChangeChairState();
                StartCoroutine(NPCMove(Door.position));
            }
        }
    }
}
