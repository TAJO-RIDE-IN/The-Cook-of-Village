using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TableChair
{
    public int chairCount = 0;
    public Vector3[] chairPos = new Vector3[4];
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
    public GameObject noticeUI;

    private bool gridOn;
    private GameObject pendingObject;
    private String currentObjectName;
    private bool[] isUsedTable;
    private bool[] isUsedChair;
    private bool[] isUsedDeco;
    private TableChair currentData;
    private bool isFirst;
    private float firstDis;
    private float secondDis;
    private int selectedIndex;
    private int uiValue;
    private Vector3 pos;
    private RaycastHit hit;
    private InstallData _installData;
    private ToolPooling _toolPooling;

    [SerializeField] private LayerMask layerMask;
    private void Start()
    {
        _installData = InstallData.Instance;
        _toolPooling = ToolPooling.Instance;
        //책상, 의자, 가구 순으로 설치
        for (int i = 0; i < _installData.tableData.tableVector.Count; i++)
        {
            //GetAndPosition();
        }
    }

    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;
            for (int i = 1; i < 3; i++) //의자면 
            {
                if (currentObjectName == FurniturePooling.Instance.FurnitureDatas[i].name)
                {
                    CheckChairPosition();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uiValue--;
                Cancel();
            }
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("실행");
                PlaceObject(currentObjectName);
            }
        }
    }
    

    private void FixedUpdate()
    {
        if (pendingObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                pos = hit.point;
                //Debug.Log(hit.transform.name);
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
                Debug.Log(secondDis);
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

    private void Cancel()
    {
        FurniturePooling.Instance.ReturnObject(pendingObject, currentObjectName);
        noticeUI.SetActive(false);
        pendingObject = null;
    }

    public void UseFurniture(String name)
    {
        if (ChairNameCheck(name))
        {
            for (int j = 0; j < tableChairs.Count; j++)//의자를 설치할 수 있는지 확인하는 과정
            {
                if (tableChairs[j].chairCount < 4)
                {
                    currentObjectName = name;
                    uiValue++;
                    pendingObject = FurniturePooling.Instance.GetObject(name);
                    noticeUI.SetActive(true);
                    
                    return;
                }
            }
            StartCoroutine(TextFade(warnings[0].box,warnings[0].text));
            return;
        }
        uiValue++;
        noticeUI.SetActive(true);
        currentObjectName = name;
        pendingObject = FurniturePooling.Instance.GetObject(name);
    }

    public void PlaceObject(String name)//테이블과 의자 정보는 테이블의자 클래스에 저장, 그 외는 pooledObjects에 저장
    {
        if (name == FurniturePooling.Instance.FurnitureDatas[0].name)//테이블이면 클래스 리스트 하나 추가
        {
            currentData = new TableChair();
            currentData.tablePos = pendingObject.transform.position;
            tableChairs.Add(currentData);
            pendingObject = null;
            noticeUI.SetActive(false);
            InstallData.Instance.PassVector3Data(InstallData.SortOfInstall.Table,currentData.tablePos);
            return;
        }

        if (ChairNameCheck(name))
        {
            if (secondDis < 1.5f && secondDis > 0.8f)
            {
                tableChairs[selectedIndex].chairCount++;
                InstallData.Instance.PassVector3Data(InstallData.SortOfInstall.Chair,pendingObject.transform.position, currentObjectName, selectedIndex);
                pendingObject = null;
                return;
            }
            else
            {
                StartCoroutine(TextFade(warnings[1].box,warnings[1].text));
                return;
            }
        }
        InstallData.Instance.PassVector3Data(InstallData.SortOfInstall.Furnitue,pendingObject.transform.position, currentObjectName);
        pendingObject = null;
        FurniturePooling.Instance.FindInstallPoolData(name).pooledObjects.Add(pendingObject);
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

    public void GetAndPosition(Vector3 vector3, string name, int tableNumber)
    {
        if (ChairNameCheck(name))
        {
            
        }
        /*ToolPooling.Instance.pooledObject[index] = ToolPooling.Instance.GetObject(name);
        ToolPooling.Instance.pooledObject[index].transform.position = ToolPooling.Instance.toolPosition[index].position;
        ToolPooling.Instance.pooledObject[index].transform.rotation = ToolPooling.Instance.toolPosition[index].rotation;
        ToolPooling.Instance.pooledObject[index].index = index;
        isUsed[index] = true;
        PositionCollider[index].SetActive(false);
        if (isDirectInstall)
        {
            _cookingCharacter.isCookPositionCollider = false;
        }*/
    }

    private void ChairInstallation(Vector3 vector3, string name, int tableNumber)
    {
        
    }
    
}
