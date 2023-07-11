using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
 
public class KeyScene: MonoBehaviour {
    public string SceneName;

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            SceneManager.LoadScene(SceneName);
        }
    }
}