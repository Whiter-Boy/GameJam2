using UnityEngine;

// Press the space key in Play Mode to switch to the Bounce state.

public class AnimationEnemy : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("Animator23", 0, 0.25f);
            }
        }
}