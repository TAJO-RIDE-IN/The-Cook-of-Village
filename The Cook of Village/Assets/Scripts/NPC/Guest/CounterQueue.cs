using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaitingLine
{
    public Queue<GameObject> GuestQueue = new Queue<GameObject>();
    private List<Vector3> PositionList = new List<Vector3>();
    public void AddPositionList(List<Vector3> positionList)
    {
        this.PositionList = positionList;
    }
    private bool CanAddGuest()
    {
        return GuestQueue.Count < PositionList.Count;
    }
    public Vector3 LineUPPosition(GameObject guest)
    {
        Vector3 position = PositionList[GuestQueue.ToList().IndexOf(guest)];
        return position;
    }
    public void AddGuest(GameObject guest)
    {
        if(CanAddGuest())
        {
            GuestQueue.Enqueue(guest);
        }
    }
    public void QuitGuest(GameObject guest)
    {
        if(GuestQueue.Peek() == guest)
        {
            GuestQueue.Dequeue();
            RelocateAllGuests();
        }
    }
    private void RelocateAllGuests() //NPC줄 앞으로 땡기기
    {
        foreach(GameObject guest in GuestQueue)
        {
            GuestMove move = guest.GetComponent<GuestMove>();
            move.RelocateGuest(LineUPPosition(guest));
        }
    }
}

public class CounterQueue : MonoBehaviour
{
    private Queue<Vector3> WaitingPosition = new Queue<Vector3>();
    public Transform CounterObject;
    [SerializeField]
    private Transform[] WaitingPositionArray;
    public WaitingLine waitngQueue;
    private void Start()
    {
        foreach(Transform line in WaitingPositionArray)
        {
            WaitingPosition.Enqueue(line.position);
        }
        waitngQueue = new WaitingLine();
        waitngQueue.AddPositionList(WaitingPosition.ToList());
    }

    public void GoGuest(GameObject guest)
    {
        waitngQueue.AddGuest(guest);
    }

    public void OutGuest(GameObject guest)
    {
        waitngQueue.QuitGuest(guest);
    }
    public void PayCounter()
    {
        if(waitngQueue.GuestQueue.Count > 0)
        {
            FoodOrder PayGuest = waitngQueue.GuestQueue.Peek().GetComponent<FoodOrder>();
            PayGuest.PayFood();
        }
    }
}
