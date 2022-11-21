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
        float load;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        operation.allowSceneActivation = false;
        load = (operation.progress > LoadingTime) ? operation.progress : LoadingTime;
        float time = 0;
        while (time < LoadingTime || !operation.isDone)
        {
            yield return null;
            time += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                LoadingBarFill.value = Mathf.Lerp(LoadingBarFill.value, load, time);
                if (LoadingBarFill.value >= operation.progress)
                {
                    time = 0f;
                }
            }
            else
            {
                LoadingBarFill.value = Mathf.Lerp(LoadingBarFill.value, 1f, time);
                if (LoadingBarFill.value == 1.0f)
                {
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
    }
}
