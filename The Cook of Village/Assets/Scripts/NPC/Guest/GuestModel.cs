using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestModel : GuestNPC
{
    [SerializeField]
    private GameObject[] Models;
    private GameObject CurrentModel;

    private void OnEnable()
    {
        SetNPCModel(true);
    }

    private void OnDisable()
    {
        CurrentModel.SetActive(false);
    }
    private void SetNPCModel(bool state)
    {
        if (true)
        {
            int model = Random.Range(0, Models.Length);
            CurrentModel = Models[model];
        }
        CurrentModel.SetActive(state);
    }
}
