using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorDisable : MonoBehaviour
{
    public bool DoDestroy;
    public GameObject Player;
    public GameObject DisableThis;
    public float DisableTime;

    public bool playerhaskey;

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){
            yield return new WaitForSeconds(DisableTime);
            if (playerhaskey)
            {
                DisableThis.SetActive(false);
            }
            if (DoDestroy == true){
                Destroy(this);
            }
        }
    }
}