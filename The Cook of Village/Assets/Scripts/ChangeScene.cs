using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public enum SceneToGo { Intro = 0, SceneLoad = 1, Village = 2, Restaurant = 3 }

    public SceneToGo sceneToGo;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.NextSceneIndex = (int) sceneToGo;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
