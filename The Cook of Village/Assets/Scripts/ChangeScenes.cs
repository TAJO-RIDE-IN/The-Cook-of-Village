using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void loadScene()
    {
        SceneManager.LoadScene("GameLoad");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Scene scene = SceneManager.GetActiveScene();
            GameManager.Instance.preSceneIndex = scene.buildIndex;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
