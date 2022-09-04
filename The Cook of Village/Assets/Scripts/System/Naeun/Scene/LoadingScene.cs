using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider LoadingBarFill;
    public Text TipText;
    private float LoadingTime = 2f;
    private void Awake()
    {
        StartCoroutine(LoadSceneAsync(GameManager.Instance.NextSceneIndex));
        TipText.text = Explanation.LoadingTip[Random.Range(0, Explanation.LoadingTip.Count-1)];
    }
 
    private IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        operation.allowSceneActivation = false;
        float time = 0;
        while (time < LoadingTime)
        {
            time += Time.deltaTime;
            float progressValue = Mathf.Clamp01(time / LoadingTime);
            LoadingBarFill.value = progressValue;
            if(LoadingBarFill.value == 1f) { operation.allowSceneActivation = true; }
            yield return null;
        }
    }
}
