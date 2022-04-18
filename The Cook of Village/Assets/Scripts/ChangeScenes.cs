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
        Scene scene = SceneManager.GetActiveScene();
        if (other.tag == "Player")
        {
            if(scene.name == "Restaurant")
            {
                LoadSceneVillage();
            }
            else if(scene.name == "Village")
            {
                LoadSceneRestaurant();
            }
        }
    }
    public void LoadSceneRestaurant()
    {
        SceneManager.LoadScene("Restaurant");
    }
    public void LoadSceneVillage()
    {
        SceneManager.LoadScene("Village");
    }
}
