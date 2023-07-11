using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class StaminaDrain : MonoBehaviour
{
    public float stamina;

    public CharacterController Controller;

    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Walking Stamina
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log(stamina);
            DrainStaminaWalk();
        }
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            Debug.Log(stamina);
            DrainStaminaWalk();
        }

        // Running Stamina
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log(stamina);
            DrainStaminaRun();
        }
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log(stamina);
            DrainStaminaRun();
        }






        // Gain back Stamina
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            GainStamina();
        }




        


    }
    public void DrainStaminaWalk()
    {
        if (stamina > 0.1)
        {
            stamina -= Time.deltaTime * 5;
        }

    }

    public void DrainStaminaRun()
    {
        if (stamina > 0.1)
        {
            stamina -= Time.deltaTime * 10;
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
