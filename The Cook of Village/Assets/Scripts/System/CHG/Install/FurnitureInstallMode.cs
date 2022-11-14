using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInstallMode : InstallMode
{
    public int installableTableCount;
    public int installableChairCount;
    public int installableDecoCount;

    private GameObject pendingObject;
    
    private bool[] isUsedTable;
    private bool[] isUsedChair;
    private bool[] isUsedDeco;

    private Vector3 pos;

    private RaycastHit hit;

    [SerializeField] private LayerMask layerMask;
    private void Start()
    {
        SoundManager.Instance.Play(SoundManager.Instance._audioClips["Take a Plate"]);
        isUsedTable = new bool[installableTableCount];
        isUsedChair = new bool[installableChairCount];
        isUsedDeco = new bool[installableDecoCount];
        //데이터에 따라 시작 할 때 설치
        for (int i = 0; i < InstallData.toolData._indexNames.Count; i++)
        {
            //GetAndPosition(InstallData.toolData._indexNames[i].index, InstallData.toolData._indexNames[i].name);
        }
    }

    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void UseFurniture(String name)
    {

        pendingObject = FurniturePooling.Instance.GetObject(name);
        FurniturePooling.Instance.FindInstallPoolData(name).pooledObjects
            .Add(FurniturePooling.Instance.GetObject(name));
    }

    public void PlaceObject()
    {
        pendingObject = null;
        
    }

    public override void ReceivePositionIndex(int x)
    {
        
    }

    protected override void ReturnPooledObject()
    {
        
    }

    public override void GoInstall()
    {
        
    }

    protected override void StartInstall()
    {
        
    }

    public override void CancelInstall()
    {
        
    }
    
}
