using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewLevel2 : MonoBehaviour
{
    public string levelName;
    public GameObject Player;
    public float DisableTime;
    public Behaviour EnableThis;

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){
            EnableThis.enabled = true;
            yield return new WaitForSeconds(DisableTime);
            if (!string.IsNullOrEmpty(levelName))
                SceneManager.LoadScene(levelName);
        }
    }
}