using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public enum SceneToGo { Intro, SceneLoad, Village, Restaurant }
    public SceneToGo sceneToGo;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.NextSceneIndex = (int)sceneToGo;
            SceneManager.LoadSceneAsync(1);
        }
    }
    public void SceneButtonClick()
    {
        GameManager.Instance.NextSceneIndex = (int)sceneToGo;
        SceneManager.LoadSceneAsync(1);
    }
}
