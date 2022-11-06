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
            MoveScene();
        }
    }
    public void MoveScene()
    {
        GameManager.Instance.NextSceneIndex = (int)sceneToGo;
        GameManager.Instance.LoadObject();
        SceneManager.LoadSceneAsync(1);
    }
}
