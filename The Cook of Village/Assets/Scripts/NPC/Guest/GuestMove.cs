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
    private GuestNPC.State BeforeState;
    [SerializeField]
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
        StartCoroutine(NPCMove(ChairPosition(), "Chair"));
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

    private IEnumerator NPCMove(Vector3 destination, string destination_name) //NPC�̵�
    {
        if(guest.CurrentState != GuestNPC.State.Walk)
        {
            guest.ChangeState(GuestNPC.State.Walk);
        }
        while (!isArrive)
        {
            agent.SetDestination(destination);
            if (agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 0.5f) //NPC ������ ����
            {
                agent.isStopped = true;
                NPCState(destination_name);
                isArrive = true;
            }
            yield return null;
        }
    }

    private void NPCState(string destination_name) //�������� ���� NPC���� ��ȭ
    {
        switch (destination_name)
        {
            case "Chair":
                guest.ChangeState(GuestNPC.State.Sit);
                break;
            case "Door":
                guest.ChangeState(GuestNPC.State.GoOut);
                break;
            case "Counter":
                guest.ChangeState(GuestNPC.State.Idle);
                break;
        }
    }

    private void GoDestination(GuestNPC.State state) //NPC ���¿� ���� �̵�
    {
        isArrive = false;
        agent.isStopped = false;
        switch (state)
        {
            case GuestNPC.State.StandUP:
                ChangeChairState();
                Transform Destination = (BeforeState == GuestNPC.State.Eat) ? Counter : Door;
                StartCoroutine(NPCMove(Destination.position, Destination.name));
                break;
            case GuestNPC.State.Pay:
                StartCoroutine(NPCMove(Door.position, Door.ToString()));
                break;
            default:
                break;
        }
    }

    public void AddObserver() //MonoBehaviour ������ new ���Ұ�
    {
        guest.AddObserver(this);
    }

    public void Change(GuestNPC obj) //observer �ٲ� �� ����
    {
        if(obj is GuestNPC)
        {
            var guestNPC = obj;
            GoDestination(guestNPC.CurrentState);
            BeforeState = guestNPC.CurrentState;
        }
    }
}
