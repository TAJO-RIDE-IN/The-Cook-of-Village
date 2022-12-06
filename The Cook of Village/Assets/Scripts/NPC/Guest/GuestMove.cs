using System;
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
    protected GuestNPC guest;
    protected Transform Sit;
    private int VillageSitNum;
    public CounterQueue counter;

    private Coroutine MoveCoroutine;
    private bool NPCEat = false;
    [SerializeField]
    private bool isArrive = false;
    private void Awake()
    {
        chairContainer = this.gameObject.transform.parent.GetComponent<NPCPooling>();
        agent = GetComponent<NavMeshAgent>();
        guest = GetComponent<GuestNPC>();
        if (guest.currentNPC.Equals(GuestNPC.Guest.Villge))
        {
            VillageGuest VillageNPC = this.gameObject.GetComponent<VillageGuest>();
            VillageSitNum = (int)VillageNPC.npcInfos.work;
        }
        AddObserver(guest);
    }
    private void OnEnable()
    {
        ChairUse();
        Move(guest.chairUse.CloseDestination(transform).position, "Chair");
    }
    protected virtual void OutChair()
    {
        chairContainer.UseChair.Remove(UseChair);
        UseChair = null;
    }

    protected virtual void ChairUse() //사용하지 않은 의자의 위치을 랜덤으로 출력, 사용중인 의자 저장
    {
        int chairNum = UnityEngine.Random.Range(0, chairContainer.WaitChair.Count);
        UseChair = chairContainer.WaitChair[chairNum];
        guest.chairUse = UseChair.GetComponent<ChairUse>();
        Sit = guest.chairUse.SitPosition(guest.currentNPC, VillageSitNum);
        chairContainer.UseChair.Add(UseChair);
    }

    private void Move(Vector3 destination, string destination_name) //agent 초기화와 콜라이더 저장을 위한 함수
    {
        agent.enabled = false;
        agent.enabled = true;
        if (MoveCoroutine != null)
        {
            StopCoroutine(MoveCoroutine);
        }
        MoveCoroutine = StartCoroutine(NPCMove(destination, destination_name));
    }

    private IEnumerator NPCMove(Vector3 destination, string destination_name) //NPC이동
    {
        float stopDistance = (destination_name.Equals("Chair")) ? 0.4f : 0.3f;
        isArrive = false;
        agent.enabled = true;
        guest.ChangeState(GuestNPC.State.Walk);
        while (!isArrive)
        {
            agent.SetDestination(destination);
            if (!agent.pathPending) //가까이 갔을때 감지
            {
                if (agent.remainingDistance <= agent.stoppingDistance + stopDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude >= 0.2f * 0.2f)
                    {
                        agent.enabled = false;
                        NPCState(destination_name);
                        isArrive = true;
                    }
                }
            }
            yield return null;
        }
    }
    protected void NPCLook(Vector3 lookPosition)
    {
        Vector3 table = new Vector3(lookPosition.x, this.transform.position.y, lookPosition.z);
        transform.LookAt(table);
    }
    protected virtual void NPCState(string destination_name) //도착지에 따른 NPC상태 변화
   {
        switch (destination_name)
        {
            case "Chair":
                transform.position = Sit.position;
                NPCLook(guest.chairUse.TablePosition.position);
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
                else { StartCoroutine(WaitAnimation("StandUp", 0.8f, () => Move(Door.position, "Door"))); }
                NPCEat = false;
                OutChair();                                
                break;
            case GuestNPC.State.Pay:
                counter.OutGuest(this.gameObject);
                StartCoroutine(WaitAnimation("Pay", 0.05f, () => Move(Door.position, "Door")));
                NPCLook(counter.CounterObject.position);             
                break;
            default:
                break;
        }
    }
    private IEnumerator WaitAnimation(string AnimationName, float time, Action action) //애니메이션 재생후 액션
    {
        bool isPlayAni = true;
        while (isPlayAni)
        {
            if (guest.ModelsAni.GetCurrentAnimatorStateInfo(0).IsName(AnimationName) && guest.ModelsAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= time)
            {
                action();
                isPlayAni = false;
            }
            yield return null;
        }
    }

    private void GoCounter() //Counter 줄서기
    {
        counter.GoGuest(this.gameObject);
        Vector3 countLine = counter.waitngQueue.LineUPPosition(this.gameObject);
        StartCoroutine(WaitAnimation("StandUp", 0.8f, () => Move(countLine, "Counter")));
    }
    private void OutCounter() //Counter에서 문으로 이동
    {
        counter.OutGuest(this.gameObject);
        Move(Door.position, "Door");
    }
    public void RelocateGuest(Vector3 position) //Counter 줄 정렬
    {
        Move(position, "CounterLine");
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
            if(guestNPC.CurrentState.Equals(GuestNPC.State.Eat))
            {
                NPCEat = true;
            }
            GoDestination(guestNPC.CurrentState);
        }
    }
}
