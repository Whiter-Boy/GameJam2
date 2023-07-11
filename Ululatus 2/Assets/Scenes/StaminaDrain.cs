using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using TMPro;
using UnityEngine.UI;

public class StaminaDrain : MonoBehaviour
{
    public float stamina;

    public GameObject player;

    public Text staminaText;

    private bool outOfStamina;


    // Start is called before the first frame update
    void Start()
    {
        outOfStamina = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Walking Stamina
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            if (outOfStamina == false)
            {
                DrainStaminaWalk();
            }
        }
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            if (outOfStamina == false)
            {
                DrainStaminaWalk();
            }

        }

        // Running Stamina
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 && Input.GetKey(KeyCode.LeftShift))
        {

            DrainStaminaRun();
        }
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 && Input.GetKey(KeyCode.LeftShift))
        {

            if (outOfStamina == false)
            {
                DrainStaminaRun();
            }
            
        }

        staminaText.text ="Stamina: " + Mathf.Round(stamina) + "%";




        // Gain back Stamina
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            GainStamina();
        }


        // check if player has over 15 stamina to enable movement
        if (stamina > 15)
        {
            player.gameObject.GetComponent<FirstPersonController>().AbleToMove();
            outOfStamina = false;
        }


        // disables player walking on 0 stamina
        if (stamina < 0.1)
        {
            outOfStamina = true;
            player.gameObject.GetComponent<FirstPersonController>().NoStamina();
            
        }




    }
    public void DrainStaminaWalk()
    {
        if (stamina > 0.1)
        {
            stamina -= Time.deltaTime * 3;
        }

    }

    public void DrainStaminaRun()
    {
        if (stamina > 0.1)
        {
            stamina -= Time.deltaTime * 6;
        }

    }

    public void GainStamina()
    {
        if (stamina < 100)
        {
            stamina += Time.deltaTime * 5;
        }

    }

    


}