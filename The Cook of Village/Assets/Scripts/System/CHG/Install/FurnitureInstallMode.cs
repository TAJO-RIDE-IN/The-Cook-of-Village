using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TableChair
{
    public int chairCount = 0;
    public Vector3 tablePos;
}

[Serializable]
public class Warning
{
    public GameObject box;
    public Text text;
}
public class FurnitureInstallMode : InstallMode
{
    public List<TableChair> tableChairs = new List<TableChair>();
    public List<Warning> warnings = new List<Warning>();
    public float chairTableDis;
    public GameObject[] noticeUI;
    public bool isActive;

    private bool gridOn;
    private GameObject pendingObject;
    private String currentObjectName;
    private bool[] isUsedTable;
    private bool[] isUsedChair;
    private bool[] isUsedDeco;
    private bool isFirst;
    private bool isDelete;
    private TableChair currentData;
    
    private float firstDis;
    private float secondDis;
    private int selectedIndex;
    private GameObject objectToInstall;
    private GameObject beforObjectToDelete;
    private GameObject objectToDelete;
    private int uiValue;
    private Vector3 pos;
    private RaycastHit hit;
    
    private GameData gameData;
    
    
    [HideInInspector] public InstallData _installData;
    [HideInInspector] public FurniturePooling _furniturePooling;
    private ItemInfos _itemInfos;
    private InstalledData _installedData;

    [SerializeField] private LayerMask installLayer;
    [SerializeField] private LayerMask deleteLayer;
    private void Start()
    {
        gameData = GameData.Instance;
        
        
        //책상, 의자, 가구 순으로 설치
    }

    public void InstallWhenStart()
    {
        for (int i = 0; i < _installData.tableData.vector.Count; i++)
        {
            GetAndPosition(_installData.tableData.vector[i], _furniturePooling.poolObjectData[0].name);
        }

        for (int i = 0; i < _installData.chairData.positionNames.Count; i++)
        {
            GetAndPosition(_installData.chairData.positionNames[i].vector3,
                _installData.chairData.positionNames[i].name,
                _installData.chairData.positionNames[i].tableIndex);
        }
        for (int i = 0; i < _installData.furnitureData.vecRotNames.Count; i++)
        {
            GetAndPosition(_installData.furnitureData.vecRotNames[i].vector,
                _installData.furnitureData.vecRotNames[i].name);
        }
    }
    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;
            for (int i = 1; i < 3; i++) //의자면 
            {
                if (currentObjectName == FurniturePooling.Instance.InstalledData[i].name)
                {
                    CheckChairPosition();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uiValue--;
                Cancel();
                isActive = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("실행");
                PlaceObject(currentObjectName);
            }
        }
        if (isDelete)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (objectToDelete != null)
                {
                    InstalledData installedData = _furniturePooling.FindInstallName(objectToDelete);
                    if (installedData != null)
                    {
                        installedData.pooledObjects.Remove(objectToDelete);
                        _furniturePooling.ReturnObject(objectToDelete, installedData.name);
                        _installData.DeleteVector3Data(objectToDelete.transform.position,
                            FindSortOfInstall(installedData));

                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FinishDelete();
                isActive = false;
            }
        }
    }

    private InstallData.SortOfInstall FindSortOfInstall(InstalledData installedData)
    {
        if (installedData.name == FurniturePooling.Instance.InstalledData[0].name)
        {
            return InstallData.SortOfInstall.Table;
        }

        if (ChairNameCheck(installedData.name))
        {
            return InstallData.SortOfInstall.Chair;
        }

        return InstallData.SortOfInstall.Furnitue;
    }
    private void FixedUpdate()
    {
        if (pendingObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, installLayer))
            {
                pos = hit.point;
                return;
                //Debug.Log(hit.transform.name);
            }
        }

        if (isDelete)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, deleteLayer))
            {
                objectToDelete = hit.transform.gameObject;
                if (beforObjectToDelete != null)
                {
                    if (objectToDelete != beforObjectToDelete)
                    {
                        beforObjectToDelete.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                }
                beforObjectToDelete = objectToDelete;
                objectToDelete.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                return;
            }
            else
            {
                if (objectToDelete != null)
                {
                    objectToDelete.transform.localScale = new Vector3(1f, 1f, 1f);
                    objectToDelete = null;
                }
                
            }
        }
    }


    private bool ChairNameCheck(String name)
    {
        for (int i = 1; i < 3; i++)//의자만 특수한 UI 띄움
        {
            if (name == FurniturePooling.Instance.poolObjectData[i].name)
            {
                return true;
            }
        }
        return false;
    }

    private void CheckChairPosition()
    {
        firstDis = Vector3.Distance(pendingObject.transform.position, tableChairs[0].tablePos);
        selectedIndex = 0;
        
        for (int j = 0; j < tableChairs.Count; j++)
        {
            if (tableChairs[j].chairCount < 4)
            {
                secondDis = Vector3.Distance(pendingObject.transform.position, tableChairs[j].tablePos);//테이블과의 거리차이
                //Debug.Log(secondDis);
                if (secondDis < firstDis)
                {
                    firstDis = secondDis;
                    selectedIndex = j;
                }
            }
        }
        pendingObject.transform.LookAt(new Vector3(tableChairs[selectedIndex].tablePos.x,
            tableChairs[selectedIndex].tablePos.y, tableChairs[selectedIndex].tablePos.z));
    }

    
    public void UseFurniture(ItemInfos infos)
    {
        _itemInfos = infos;
        if (ChairNameCheck(infos.Name))
        {
            for (int j = 0; j < tableChairs.Count; j++)//의자를 설치할 수 있는지 확인하는 과정
            {
                if (tableChairs[j].chairCount < 4)
                {
                    currentObjectName = infos.Name;
                    uiValue++;
                    pendingObject = FurniturePooling.Instance.GetObject(infos.Name);
                    noticeUI[0].SetActive(true);
                    isActive = true;
                    return;
                }
            }
            StartCoroutine(TextFade(warnings[0].box,warnings[0].text));
            return;
        }
        
        uiValue++;
        noticeUI[0].SetActive(true);
        currentObjectName = infos.Name;
        isActive = true;
        pendingObject = FurniturePooling.Instance.GetObject(infos.Name);
    }

    public void PlaceObject(String name)//테이블과 의자 정보는 테이블의자 클래스에 저장, 그 외는 pooledObjects에 저장
    {
        if (name == FurniturePooling.Instance.InstalledData[0].name)//테이블이면 클래스 리스트 하나 추가
        {
            //gameData.Fame += _itemInfos.
            currentData = new TableChair();
            currentData.tablePos = pendingObject.transform.position;
            tableChairs.Add(currentData);
            pendingObject.layer = 10;
            noticeUI[0].SetActive(false);
            gameData.ChangeFame(3);
            InstallData.Instance.PassVector3Data(InstallData.SortOfInstall.Table,currentData.tablePos);
            pendingObject = null;
            _installedData = _furniturePooling.FindInstallPoolData(name);
            _installedData.pooledObjects.Add(pendingObject);
            return;
        }

        if (ChairNameCheck(name))
        {
            if (secondDis < 1.5f && secondDis > 0.8f)
            {
                pendingObject.layer = 10;
                tableChairs[selectedIndex].chairCount++;
                gameData.ChangeFame(5);
                InstallData.Instance.PassVector3Data(InstallData.SortOfInstall.Chair,pendingObject.transform.position, currentObjectName, selectedIndex);
                noticeUI[0].SetActive(false);
                _installedData = _furniturePooling.FindInstallPoolData(name);
                _installedData.pooledObjects.Add(pendingObject);
                pendingObject = null;
                return;
            }
            else
            {
                StartCoroutine(TextFade(warnings[1].box,warnings[1].text));
                return;
            }
        }
        _installedData = _furniturePooling.FindInstallPoolData(name);
        _installedData.pooledObjects.Add(pendingObject);
        pendingObject.layer = 10;
        gameData.ChangeFame(20);
        InstallData.Instance.PassVecRotData(InstallData.SortOfInstall.Furnitue, pendingObject.transform.position,
            pendingObject.transform.localEulerAngles, currentObjectName);
        FurniturePooling.Instance.FindInstallPoolData(name).pooledObjects.Add(pendingObject);
        pendingObject = null;
        noticeUI[0].SetActive(false);
    }


    
    private void GetAndTransform(Transform trans, String name)
    {
        _installedData = _furniturePooling.FindInstallPoolData(name);
        objectToInstall = _furniturePooling.GetObject(name);
        objectToInstall.transform.position = trans.position;
        objectToInstall.transform.rotation = trans.rotation;
        objectToInstall.layer = 10;
        _installedData.pooledObjects.Add(objectToInstall);
    }

    private void GetAndPosition(Vector3 vector3, string name, int tableNumber = 0)
    {
        _installedData = _furniturePooling.FindInstallPoolData(name);
        objectToInstall = _furniturePooling.GetObject(name);
        objectToInstall.transform.position = vector3;
        objectToInstall.layer = 10;
        _installedData.pooledObjects.Add(objectToInstall);
        
        if (ChairNameCheck(name))
        {
            objectToInstall.transform.LookAt(tableChairs[tableNumber].tablePos);
            tableChairs[tableNumber].chairCount++;
            return;
        }

        if (name == _furniturePooling.poolObjectData[0].name)
        {
            currentData = new TableChair();
            currentData.tablePos = vector3;
            tableChairs.Add(currentData);
        }
    }

    public void StartDelete()
    {
        noticeUI[1].SetActive(true);
        isDelete = true;
        isActive = true;
    }

    public void FinishDelete()
    {
        noticeUI[1].SetActive(false);
        isDelete = false;
        isActive = false;
    }
    public IEnumerator TextFade(GameObject box, Text text)
    {
        box.SetActive(true);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        box.SetActive(false);
    }
    private void Cancel()
    {
        FurniturePooling.Instance.ReturnObject(pendingObject, currentObjectName);
        noticeUI[0].SetActive(false);
        _itemInfos.Amount++;
        pendingObject = null;
    }


    
}
