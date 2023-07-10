using UnityEngine;
using System.Collections;
 
public class UnLockMouseCursor : MonoBehaviour
{
 
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
 
    // Update is called once per frame
    void Update()
    {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }