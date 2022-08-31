using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public enum SceneToGo { Intro = 0, SceneLoad = 1, Village = 2, Restaurant = 3 }
    public SceneToGo sceneToGo;
    public Image LoadingBarFill;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.NextSceneIndex = (int)sceneToGo;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
    public void SceneButtonClick()
    {
            GameManager.Instance.NextSceneIndex = (int)sceneToGo;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    private IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
