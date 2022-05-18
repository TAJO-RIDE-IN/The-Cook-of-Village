using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{
    public Slider slider;
    bool IsDone = false;
    float fTime = 0f;
    AsyncOperation async_operation;
    private void Start()
    {
        if (GameManager.Instance.preSceneIndex == 2)
        {
            StartCoroutine(StartLoad(3));
        }
        else
        {
            StartCoroutine(StartLoad(2));
        }
        
    }

    void Update()
    {
        fTime += Time.deltaTime;
        slider.value = fTime;
 
        if (fTime >= 3)
        {
            async_operation.allowSceneActivation = true;
        }
    }
 
    public IEnumerator StartLoad(int SceneIndex)
    {
        async_operation = SceneManager.LoadSceneAsync(SceneIndex);
        async_operation.allowSceneActivation = false;
 
        if (IsDone == false)
        {
            IsDone = true;
 
            while (async_operation.progress < 0.9f)
            {
                slider.value = async_operation.progress;
 
                yield return null;
            }
        }
    }
}