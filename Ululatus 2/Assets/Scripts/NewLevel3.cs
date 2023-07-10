using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewLevel3 : MonoBehaviour
{
    public string levelName;
    public float DisableTime;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(DisableTime);
        if (!string.IsNullOrEmpty(levelName))
            SceneManager.LoadScene(levelName);
    }
}