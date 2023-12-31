using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText_Trigger : MonoBehaviour
{
    public bool DoStart;
    public Text PlayerDialogue;
    public GameObject Background;
    public string givenText;
    public string givenStartText;
    //private Text UIText;

    IEnumerator Start(){
        if(DoStart == true);
        {
            //PlayerDialogue.text = "The car battery is dead. I gotta find a shelter.";
            Background.SetActive(true);
            PlayerDialogue.text = givenStartText;
            yield return new WaitForSeconds(5);
            PlayerDialogue.text = "";
            Background.SetActive(false);
        }
    }

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){
            Background.SetActive(true);
            PlayerDialogue.text = givenText;
            yield return new WaitForSeconds(8);
            PlayerDialogue.text = "";
            Background.SetActive(false);
            Destroy(this);
        }
    }
}