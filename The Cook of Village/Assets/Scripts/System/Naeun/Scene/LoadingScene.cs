using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{
    public Slider LoadingBarFill;
    private void Start()
    {
        StartCoroutine(LoadSceneAsync(GameManager.Instance.NextSceneIndex));
    }
 
    private IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.value = progressValue;
            if(LoadingBarFill.value == 1f) { operation.allowSceneActivation = true; }
            yield return null;
        }
    }
}
