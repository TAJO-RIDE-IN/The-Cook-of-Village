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
        isArrive = false;
        agent.enabled = true;
        guest.ChangeState(GuestNPC.State.Walk);
        while (!isArrive)
        {
            agent.SetDestination(destination);
            if (agent.velocity.sqrMagnitude >= 1f && agent.remainingDistance <= 0.1f) //NPC ������ ����
            {
                agent.enabled = false;
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
                Vector3 table = new Vector3(UseChair.transform.parent.position.x, transform.position.y, UseChair.transform.parent.position.z);
                Transform chair = UseChair.transform.GetChild(0);
                transform.position = new Vector3(chair.position.x, transform.position.y, chair.position.z);
                transform.LookAt(table);
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
