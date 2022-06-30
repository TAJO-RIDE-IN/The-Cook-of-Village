using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Localization : MonoBehaviour
{    public static string GetLocalizedString(string tableName, string keyName)
    {
        LocalizedString localizeString = new LocalizedString() { TableReference = tableName, TableEntryReference = keyName };
        var stringOperation = localizeString.GetLocalizedStringAsync();

        if (stringOperation.IsDone && stringOperation.Status == AsyncOperationStatus.Succeeded)
        {
            return stringOperation.Result;
        }
        else
        {
            return null;
        }
    }
}
