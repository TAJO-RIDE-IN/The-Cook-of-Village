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

    private Vector3 ChairPosition() //사용하지 않은 의자의 위치을 랜덤으로 출력, 사용중인 의자 저장
    {
        int chairNum = Random.Range(0, chairContainer.WaitChair.Count);
        Vector3 destination = chairContainer.WaitChair[chairNum].transform.position;
        UseChair = chairContainer.WaitChair[chairNum];
        chairContainer.WaitChair.RemoveAt(chairNum);
        return (destination);
    }

    private IEnumerator NPCMove(Vector3 destination, string destination_name) //NPC이동
    {
        isArrive = false;
        agent.enabled = true;
        guest.ChangeState(GuestNPC.State.Walk);
        while (!isArrive)
        {
            agent.SetDestination(destination);
            if (agent.velocity.sqrMagnitude >= 1f && agent.remainingDistance <= 0.1f) //NPC 목적지 도착
            {
                agent.enabled = false;
                NPCState(destination_name);
                isArrive = true;
            }
            yield return null;
        }
    }

    private void NPCState(string destination_name) //도착지에 따른 NPC상태 변화
    {
        switch (destination_name)
        {
            case "Chair":
                Vector3 table = new Vector3(UseChair.transform.parent.position.x, transform.position.y, UseChair.transform.parent.position.z);
                Transform chair = UseChair.transform.GetChild(0);
                transform.position = chair.position;
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

    private void GoDestination(GuestNPC.State state) //NPC 상태에 따른 이동
    {
        switch (state)
        {
            case GuestNPC.State.StandUP:
                StartCoroutine(WaitStandUP());
                break;
            case GuestNPC.State.Pay:
                StartCoroutine(NPCMove(Door.position, Door.ToString()));
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitStandUP() //일어난 후 NPC 움직이도록
    {
        bool isPlayAni = true;
        while (isPlayAni)
        {
            if (guest.ModelsAni.GetCurrentAnimatorStateInfo(0).IsName("StandUp") && guest.ModelsAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                Transform Destination = (BeforeState == GuestNPC.State.Eat) ? Counter : Door;
                StartCoroutine(NPCMove(Destination.position, Destination.name));
                ChangeChairState();
                isPlayAni = false;
            }
            yield return null;
        }
    }

    public void AddObserver() //MonoBehaviour 때문에 new 사용불가
    {
        guest.AddObserver(this);
    }

    public void Change(GuestNPC obj) //observer 바뀐 값 받음
    {
        if(obj is GuestNPC)
        {
            var guestNPC = obj;
            GoDestination(guestNPC.CurrentState);
            BeforeState = guestNPC.CurrentState;
        }
    }
}
