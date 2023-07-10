using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour
{
    public string levelName;
    public GameObject Player;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){
            if (!string.IsNullOrEmpty(levelName))
                SceneManager.LoadScene(levelName);
            else
                Debug.Log("Please write a scene name in the 'levelName' field of the script and don't forget to add that scene in the Build Settings!");
        }
    }
}