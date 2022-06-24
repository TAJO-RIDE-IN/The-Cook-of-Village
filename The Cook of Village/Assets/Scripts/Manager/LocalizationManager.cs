using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    private Dictionary<string, string> localizedText;
    private string missingTextString = "Localized text not found";

    void Awake()
    {

        //싱글톤 패턴
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        filePath += ".txt";
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath); //json파일을 읽어서 string으로 뽑음
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);    //deserialization

            //전체 아이템들에 대해서
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            //localizationText 데이터 불러오기 완료
            //Debug.Log("Data loaded. Dictionary containts :" + localizedText.Count + " entries");

        }
        else
        {
            //파일이 존재하지않음
            //Debug.LogError("Cannot find file");
        }

    }


    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;
    }
}
