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
    private GuestNPC npc;
    private bool isArrive = false;
    private void Awake()
    {
        chairContainer = this.gameObject.transform.parent.GetComponent<NPCPooling>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        npc = this.gameObject.GetComponent<GuestNPC>();
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
        npc.ChangeState(GuestNPC.State.Walk);
        while (!isArrive)
        {
            agent.SetDestination(destination);
            if (agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 0.5f)
            {
                npc.ChangeState(GuestNPC.State.Sit);
                isArrive = true;
            }
            yield return null;
        }
    }
    public void AddObserver(GuestNPC obj) //MonoBehaviour ������ new ���Ұ�
    {
        obj.AddObserver(this);
    }

    public void Change(GuestNPC obj)
    {
        if(obj is GuestNPC)
        {
            var guestNPC = obj;
            if(obj.CurrentState == GuestNPC.State.StandUP)
            {
                ChangeChairState();
                StartCoroutine(NPCMove(Door.position));
            }
        }
    }
}
