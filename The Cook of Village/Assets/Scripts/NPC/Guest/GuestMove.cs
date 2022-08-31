using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GuestMove : MonoBehaviour, IObserver<GuestNPC>
{
    public Transform Door;
    private GameObject UseChair;
    private NPCPooling chairContainer;
    private NavMeshAgent agent;
    private GuestNPC guest;
    public CounterQueue counter;

    private bool NPCEat = false;
    [SerializeField]
    private bool isArrive = false;
    private void Awake()
    {
        chairContainer = this.gameObject.transform.parent.GetComponent<NPCPooling>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        guest = this.gameObject.GetComponent<GuestNPC>();
        AddObserver(guest);
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
            if (agent.velocity.sqrMagnitude >= 1.3f && agent.remainingDistance <= 0.1f) //NPC 목적지 도착
            {
                agent.enabled = false;
                NPCState(destination_name);
                isArrive = true;
            }
            yield return null;
        }
    }

    private Transform SitPosition(GuestNPC.Guest who)//앉는 위치
    {
        Transform chair = (who == GuestNPC.Guest.General) ? UseChair.transform.GetChild(0) : UseChair.transform.GetChild(1);
        transform.position = chair.position;
        return chair;
    }

    private void NPCState(string destination_name) //도착지에 따른 NPC상태 변화
   {
        switch (destination_name)
        {
            case "Chair":
                Transform chair = SitPosition(guest.currentNPC);
                Vector3 table = new Vector3(UseChair.transform.parent.position.x, chair.transform.position.y, UseChair.transform.parent.position.z);               
                transform.LookAt(table);
                guest.ChangeState(GuestNPC.State.Sit);
                break;
            case "Door":
                guest.ChangeState(GuestNPC.State.GoOut);
                break;
            case "Counter":
                guest.ChangeState(GuestNPC.State.Idle);
                StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.PayWaitingTime, () => OutCounter())); //일정 시간 이후 나감
                break;
            case "CounterLine":
                guest.ChangeState(GuestNPC.State.Idle);
                break;
        }
    }

    private void GoDestination(GuestNPC.State state) //NPC 상태에 따른 이동
    {
        switch (state)
        {
            case GuestNPC.State.StandUP:
                if(NPCEat) { GoCounter(); }
                else { StartCoroutine(WaitAnimation("StandUp", Door.position, "Door", 0.8f)); }
                NPCEat = false;
                ChangeChairState();                                
                break;
            case GuestNPC.State.Pay:
                counter.OutGuest(this.gameObject);
                StartCoroutine(WaitAnimation("Pay", Door.position, "Door", 0.1f));
                Vector3 look = new Vector3(counter.CounterObject.position.x, transform.position.y, counter.CounterObject.position.z);
                transform.LookAt(look);               
                break;
            default:
                break;
        }
    }
    private IEnumerator WaitAnimation(string AnimationName, Vector3 Destination, string destination_name, float time) //애니메이션 재생후 NPC 움직이도록
    {
        bool isPlayAni = true;
        while (isPlayAni)
        {
            if (guest.ModelsAni.GetCurrentAnimatorStateInfo(0).IsName(AnimationName) && guest.ModelsAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= time)
            {
                StartCoroutine(NPCMove(Destination, destination_name));
                isPlayAni = false;
            }
            yield return null;
        }
    }

    private void GoCounter() //Counter 줄서기
    {
        counter.GoGuest(this.gameObject);
        StartCoroutine(WaitAnimation("StandUp", counter.waitngQueue.LineUPPosition(this.gameObject), "Counter", 0.8f));
    }
    private void OutCounter() //Counter에서 문으로 이동
    {
        counter.OutGuest(this.gameObject);
        StartCoroutine(NPCMove(Door.position, "Door"));
    }
    public void RelocateGuest(Vector3 position) //Counter 줄 정렬
    {
        StartCoroutine(NPCMove(position, "CounterLine"));
    }


    public void AddObserver(IGuestOb o)
    {
        o.AddObserver(this);
    }

    public void Change(GuestNPC obj) //observer 바뀐 값 받음
    {
        if(obj is GuestNPC)
        {
            var guestNPC = obj;
            if(guestNPC.CurrentState == GuestNPC.State.Eat)
            {
                NPCEat = true;
            }
            GoDestination(guestNPC.CurrentState);
        }
    }
}
