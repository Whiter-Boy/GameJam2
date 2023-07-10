using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    public Transform Player;
    public float posx = 1.0f;
    public float posy = 1.0f;
    public float posz = 1.0f;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){
            Player.position = new Vector3(posx, posy, posz);
            //transform.position = new Vector3(-3.327711f, 1.0f, -9.162267f);
        }
    }
}