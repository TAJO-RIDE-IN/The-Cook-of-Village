using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingFather : MonoBehaviour
{
    public enum FatherState { Idle, Walk, Nod }
    private FatherState fatherState;
    public FatherState currentState 
    { 
        get { return fatherState; }
        set
        {
            fatherState = value;
            CheckState();
        }
    }
    public Animator FatherAnimator;
    public Transform[] ArrivalTransform;
    public EndingController endingController;
    private float Speed = 1.5f;
    public IEnumerator FatherAppearance(int ArrivalIndex)
    {
        bool Arrival = false;
        currentState = FatherState.Walk;
        transform.LookAt(ArrivalTransform[ArrivalIndex]);
        while (!Arrival)
        {
            transform.position = Vector3.MoveTowards(transform.position, ArrivalTransform[ArrivalIndex].position, Speed * Time.deltaTime);
            float Dist = Vector3.Distance(transform.position, ArrivalTransform[ArrivalIndex].position);
            if(Dist < 0.05f)
            {
                Arrival = true;
                ArrivalDestination(ArrivalIndex);
            }
            yield return null;
        }
    }
    private void ArrivalDestination(int ArrivalIndex)
    {
        if(ArrivalIndex.Equals(0))
        {
            endingController.NextEvent();
        }
        else
        {
            this.gameObject.SetActive(false);
            endingController.NextDialogue();
        }
        currentState = FatherState.Idle;

    }
    private void CheckState()
    {
        switch(currentState)
        {
            case FatherState.Idle:
                FatherAnimator.SetBool("Walk", false);
                break;
            case FatherState.Walk:
                FatherAnimator.SetBool("Walk", true);
                break;
            case FatherState.Nod:
                FatherAnimator.SetTrigger("Nod");
                break;
        }
    }
}
