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
    public GameObject[] noticeUI;
    public float rotateSpeed;
    public bool isActive;
    public GameObject InstallUI;


    public GameObject pendingObject;
    private String currentObjectName;
    private bool[] isUsedTable;
    private bool[] isUsedChair;
    private bool[] isUsedDeco;
    private bool isFirst;
    private bool isDelete;
    private bool isRotation;
    private TableChair currentData;

    private float xRotate;
    private float firstDis;
    private float secondDis;
    private int selectedIndex;
    private GameObject objectToInstall;
    private GameObject beforObjectToDelete;
    private GameObject objectToDelete;
    private Vector3 pos;
    private RaycastHit hit;
    
    private GameData gameData;
    
    
    [HideInInspector] public InstallData _installData;
    [HideInInspector] public FurniturePooling _furniturePooling;
    private ItemInfos _itemInfos;

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
            GetAndTransform(_installData.furnitureData.vecRotNames[i].vector,
                _installData.furnitureData.vecRotNames[i].rotation,
                _installData.furnitureData.vecRotNames[i].name);
        }
    }
    void Update()
    {
        if (pendingObject != null)
        {
            if (ChairNameCheck(currentObjectName))
            {
                CheckChairPosition();
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Cancel();
                    isActive = false;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    //Debug.Log("실행");
                    PlaceObject(currentObjectName);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
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
                    MultipleObjectPoolingNo.PoolObjectData data = _furniturePooling.FindPooledObject(objectToDelete);
                    if (data != null)
                    {
                        data.pooledObjects.Remove(objectToDelete);
                        _furniturePooling.ReturnObject(objectToDelete, data.name);
                        _installData.DeleteVector3Data(objectToDelete.transform.position, FindSortOfInstall(data));

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

    private InstallData.SortOfInstall FindSortOfInstall(MultipleObjectPoolingNo.PoolObjectData data)
    {
        if (data.name == _furniturePooling.poolObjectData[0].name)
        {
            return InstallData.SortOfInstall.Table;
        }

        if (ChairNameCheck(data.name))
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
                if (isRotation)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        pendingObject.transform.localEulerAngles = new Vector3(
                            pendingObject.transform.localEulerAngles.x,
                            pendingObject.transform.localEulerAngles.y - rotateSpeed,
                            pendingObject.transform.localEulerAngles.z);
                        return;
                    }
                    if(Input.GetKey(KeyCode.E))
                    {
                        pendingObject.transform.localEulerAngles = new Vector3(
                            pendingObject.transform.localEulerAngles.x,
                            pendingObject.transform.localEulerAngles.y + rotateSpeed,
                            pendingObject.transform.localEulerAngles.z);
                    }
                    else
                    {
                        pendingObject.transform.position = hit.point;
                    }
                }
                else
                {
                    pendingObject.transform.position = hit.point;
                }
                return;
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

    public void UseFurniture12(String name)
    {
        if (ChairNameCheck(name))
        {
            for (int j = 0; j < tableChairs.Count; j++)//의자를 설치할 수 있는지 확인하는 과정
            {
                if (tableChairs[j].chairCount < 4)
                {
                    TurnOnNoticeUI(0);
                    currentObjectName = name;
                    pendingObject = FurniturePooling.Instance.GetObject(name);
                    
                    return;
                }
            }
            StartCoroutine(TextFade(warnings[0].box,warnings[0].text));
            return;
        }
        TurnOnNoticeUI(2);
        isRotation = true;
        currentObjectName = name;
        isActive = true;
        pendingObject = FurniturePooling.Instance.GetObject(name);
        ChangeLayerToInstallPlace();
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
                    TurnOnNoticeUI(0);
                    currentObjectName = infos.Name;
                    pendingObject = FurniturePooling.Instance.GetObject(infos.Name);
                    
                    return;
                }
            }
            StartCoroutine(TextFade(warnings[0].box,warnings[0].text));
            return;
        }
        TurnOnNoticeUI(2);
        isRotation = true;
        currentObjectName = infos.Name;
        isActive = true;
        pendingObject = FurniturePooling.Instance.GetObject(infos.Name);
        ChangeLayerToInstallPlace();
    }


    public void PlaceObject(String name)//테이블과 의자 정보는 테이블의자 클래스에 저장, 그 외는 pooledObjects에 저장
    {
        if (name == FurniturePooling.Instance.poolObjectData[0].name)//테이블이면 클래스 리스트 하나 추가
        {
            //gameData.Fame += _itemInfos.
            currentData = new TableChair();
            currentData.tablePos = pendingObject.transform.position;
            tableChairs.Add(currentData);
            pendingObject.layer = 10;
            TurnOffNoticeUI(2);
            gameData.ChangeFame(3);
            InstallData.Instance.PassVector3Data(InstallData.SortOfInstall.Table,currentData.tablePos);
            pendingObject = null;
            AddPooledObject(name, pendingObject);
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
                TurnOffNoticeUI(0);
                AddPooledObject(name, pendingObject);
                pendingObject = null;
                return;
            }
            else
            {
                StartCoroutine(TextFade(warnings[1].box,warnings[1].text));
                return;
            }
        }
        AddPooledObject(name, pendingObject);
        pendingObject.layer = 10;
        gameData.ChangeFame(20);
        InstallData.Instance.PassVecRotData(InstallData.SortOfInstall.Furnitue, pendingObject.transform.position,
            pendingObject.transform.localEulerAngles, currentObjectName);
        pendingObject = null;
        TurnOffNoticeUI(2);
        ChangeLayerToDeleteObject();
    }

    private void AddPooledObject(String name, GameObject gameObject)
    {
        _furniturePooling.FindPoolObjectData(name).pooledObjects.Add(gameObject);
    }

    private void TurnOnNoticeUI(int i)
    {
        noticeUI[i].SetActive(true);
    }
    private void TurnOffNoticeUI(int i)
    {
        noticeUI[i].SetActive(false);
    }
    
    private void GetAndTransform(Vector3 vector3, Vector3 rotation, String name)
    {
        objectToInstall = _furniturePooling.GetObject(name);
        objectToInstall.transform.position = vector3;
        objectToInstall.transform.localEulerAngles = rotation;
        objectToInstall.layer = 10;
        AddPooledObject(name, objectToInstall);
    }

    private void GetAndPosition(Vector3 vector3, string name, int tableNumber = 0)
    {
        objectToInstall = _furniturePooling.GetObject(name);
        objectToInstall.transform.position = vector3;
        objectToInstall.layer = 10;
        AddPooledObject(name, objectToInstall);
        
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

    /// <summary>
    /// 사용하기 눌렀을 때 책상이랑 의자 아니면 아일랜드테이블 레이어 InstallPlace로 바꾸기
    /// </summary>
    private void ChangeLayerToInstallPlace()
    {
        for (int i = 0; i < _furniturePooling.poolObjectData[5].pooledObjects.Count; i++)
        {
            _furniturePooling.poolObjectData[5].pooledObjects[i].layer = 9;
        }
    }

    /// <summary>
    /// 책상 레이어 원래대로
    /// </summary>
    private void ChangeLayerToDeleteObject()
    {
        for (int i = 0; i < _furniturePooling.poolObjectData[5].pooledObjects.Count; i++)
        {
            _furniturePooling.poolObjectData[5].pooledObjects[i].layer = 10;
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
        noticeUI[2].SetActive(false);
        _itemInfos.Amount++;
        pendingObject = null;
        isActive = false;
        ChangeLayerToDeleteObject();
    }

    public void InstallTabButton()
    {
        InstallUI.SetActive(true);
    }
    public void InstallTabFinishButton()
    {
        InstallUI.SetActive(false);
    }
    


    
}
