using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public bool DoStart;
    public Text PlayerDialogue;
    public GameObject Background;
    public string givenText;
    public string givenStartText;
    public int WaitTimeStart;
    public int WaitTimeNormal;
    //private Text UIText;

    IEnumerator Start(){
        if(DoStart == true);
        {
            //PlayerDialogue.text = "The car battery is dead. I gotta find a shelter.";
            Background.SetActive(true);
            PlayerDialogue.text = givenStartText;
            yield return new WaitForSeconds(WaitTimeStart);
            PlayerDialogue.text = "";
            Background.SetActive(false);
        }
    }

    IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"){
            Background.SetActive(true);
            PlayerDialogue.text = givenText;
            yield return new WaitForSeconds(WaitTimeNormal);
            PlayerDialogue.text = "";
            Background.SetActive(false);
            Destroy(this);
        }
    }
}