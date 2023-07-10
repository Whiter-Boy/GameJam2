using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Enabler : MonoBehaviour
{
    public bool DoDestroy;
    public GameObject Player;
    public GameObject DisableThis;
    public GameObject EnableThis;
    public float DisableTime;

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){
            EnableThis.SetActive(true);
            yield return new WaitForSeconds(DisableTime);
            DisableThis.SetActive(false);
            if (DoDestroy == true){
                Destroy(this);
            }
        }
    }
}