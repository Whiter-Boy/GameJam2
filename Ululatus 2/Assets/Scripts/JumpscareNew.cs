using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareNew : MonoBehaviour
{
    public GameObject JumpscareCam;
    //public AudioSource JumpscareAudio;
    public GameObject Cam1;
    //private Animator anim;
    public GameObject animpath;
    public GameObject Cam2;
    public GameObject Player;
    public string badEndingSceneName;
    public bool JumpscareCamOn;
    // Start is called before the first frame update
    void Start()
    {
        JumpscareCam.SetActive(false);
        JumpscareCamOn = false;
        //anim = animpath.GetComponent<Animator>();
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            JumpscareCamOn = true;
            if(JumpscareCamOn == true)
            {
                //anim.enabled = false;
                JumpscareCam.SetActive(true);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Player.SetActive(false);
                //JumpscareAudio.Play();
                //anim.enabled = true;
                //anim.Play("Animator23", 0, 0.25f);
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene(badEndingSceneName);
            }
        }
    }
}