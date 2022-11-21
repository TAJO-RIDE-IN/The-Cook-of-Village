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
    private GuestNPC guest;
    private Transform Sit;
    private int VillageSitNum;
    public CounterQueue counter;

    private bool NPCEat = false;
    [SerializeField]
    private bool isArrive = false;
    private void Awake()
    {
        chairContainer = this.gameObject.transform.parent.GetComponent<NPCPooling>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        guest = this.gameObject.GetComponent<GuestNPC>();
        if (guest.currentNPC == GuestNPC.Guest.Villge)
        {
            VillageGuest VillageNPC = this.gameObject.GetComponent<VillageGuest>();
            VillageSitNum = (int)VillageNPC.npcInfos.work;
        }
        AddObserver(guest);
    }
    private void OnEnable()
    {
        ChairUse();
        StartCoroutine(NPCMove(guest.chairUse.CloseDestination(transform).position, "Chair"));
    }
    private void OutChair()
    {
        chairContainer.UseChair.Remove(UseChair);
        UseChair = null;
    }

    private void ChairUse() //사용하지 않은 의자의 위치을 랜덤으로 출력, 사용중인 의자 저장
    {
        int chairNum = UnityEngine.Random.Range(0, chairContainer.WaitChair.Count);
        UseChair = chairContainer.WaitChair[chairNum];
        guest.chairUse = UseChair.GetComponent<ChairUse>();
        Sit = guest.chairUse.SitPosition(guest.currentNPC, VillageSitNum);
        chairContainer.UseChair.Add(UseChair);
    }

    private IEnumerator NPCMove(Vector3 destination, string destination_name) //NPC이동
    {
        isArrive = false;
        agent.enabled = true;
        guest.ChangeState(GuestNPC.State.Walk);
        while (!isArrive)
        {
            agent.SetDestination(destination);
            if (!agent.pathPending) //가까이 갔을때 감지
            {
                if (agent.remainingDistance <= agent.stoppingDistance + 0.3)
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

    private void NPCState(string destination_name) //도착지에 따른 NPC상태 변화
   {
        switch (destination_name)
        {
            case "Chair":
                transform.position = Sit.position;
                Vector3 table = new Vector3(guest.chairUse.TablePosition.position.x, this.transform.position.y, guest.chairUse.TablePosition.position.z);               
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
                else { StartCoroutine(WaitAnimation("StandUp", 0.8f, () => StartCoroutine(NPCMove(Door.position, "Door")))); }
                NPCEat = false;
                OutChair();                                
                break;
            case GuestNPC.State.Pay:
                StartCoroutine(WaitAnimation("Pay", 0.1f, () => StartCoroutine(NPCMove(Door.position, "Door"))));
                StartCoroutine(WaitAnimation("Pay", 0.1f, () => counter.OutGuest(this.gameObject)));
                Vector3 look = new Vector3(counter.CounterObject.position.x, transform.position.y, counter.CounterObject.position.z);
                transform.LookAt(look);               
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
        StartCoroutine(WaitAnimation("StandUp", 0.8f, () => StartCoroutine(NPCMove(countLine, "Counter"))));
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
