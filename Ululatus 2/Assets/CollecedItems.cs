using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecedItems : MonoBehaviour
{
    public bool key;
    public bool shotgun;
    public bool ammo;
    public GameObject keyobj;
    public GameObject shotgunobj;
    public GameObject player;
    public Behaviour GunSAcript;

    public GameObject lockedDoor;

    // Update is called once per frame
    void Update()
    {
        if (shotgun)
        {
            shotgunobj.SetActive(true);
            GunSAcript.GetComponent<GunModifiable>().enabled = true;
        }

        if (ammo)
        {
            GunSAcript.GetComponent<GunModifiable>().magazineSize = 30;
        }

        if (key)
        {
            lockedDoor.GetComponent<DoorDisable>().playerhaskey = true;
        }
    }
}