using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNotes : MonoBehaviour
{
    public GameObject player;
    public GameObject noteUI;
    public GameObject obj;
    public GameObject inv;
    public GameObject bc;
    public Behaviour FirstPersonController;
    public bool key;
    public bool shotgun;
    public bool ammo;

    public GameObject pickUpText;

    public AudioSource pickUpSound;

    public bool inReach;

    void Start()
    {
        //noteUI.SetActive(false);
        obj.SetActive(true);
        //inv.SetActive(true);
        pickUpText.SetActive(false);

        inReach = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = true;
            pickUpText.SetActive(true);
            //bc.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inReach)
        {
            //noteUI.SetActive(true);
            pickUpSound.Play();
            if (key == true)
            {
                pickUpText.SetActive(false);
                player.gameObject.GetComponent<CollecedItems>().key = true;
            }
            if (shotgun == true)
            {
                pickUpText.SetActive(false);
                player.gameObject.GetComponent<CollecedItems>().shotgun = true;
            }
            if (ammo == true)
            {
                pickUpText.SetActive(false);
                player.gameObject.GetComponent<CollecedItems>().ammo = true;
            }
            obj.SetActive(false);
            //inv.SetActive(false);
            //FirstPersonController.enabled = false;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
        
    }
}