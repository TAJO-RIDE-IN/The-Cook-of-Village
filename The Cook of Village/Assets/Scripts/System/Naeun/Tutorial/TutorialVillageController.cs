using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVillageController : MonoBehaviour
{
    public TutorialUI VillageTutorialUI;
    public ParticleSystem DestinationParticle;
    public List<ParticleSystem> RoadArrowParticle = new List<ParticleSystem>();
    public List<GameObject> Wall = new List<GameObject>();

    public void Start()
    {
        if(GameManager.Instance.gameMode == GameManager.GameMode.Tutorial)
        {
            Init();
        }
    }

    private void Init()
    {
        VillageTutorialUI.DialogueState(true);
        VillageTutorialUI.CallDialogue("Control");
        foreach(var obj in Wall)
        {
            obj.SetActive(true);
        }    
    }
}
