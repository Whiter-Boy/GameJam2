﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNotes : MonoBehaviour
{
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject inv;
    public GameObject bc;
    public Behaviour FirstPersonController;

    public GameObject pickUpText;

    public AudioSource pickUpSound;

    public bool inReach;

    void Start()
    {
        noteUI.SetActive(false);
        //hud.SetActive(true);
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
            bc.SetActive(false);
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
            noteUI.SetActive(true);
            pickUpSound.Play();
            //hud.SetActive(false);
            //inv.SetActive(false);
            FirstPersonController.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }


    public void ExitButton()
    {
        noteUI.SetActive(false);
        //hud.SetActive(true);
        //inv.SetActive(true);
        FirstPersonController.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
