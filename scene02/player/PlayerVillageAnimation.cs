using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVillageAnimation : MonoBehaviour {

    private Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (this.GetComponent<Rigidbody>().velocity.magnitude >= 0.2f)
        {
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }
    }
}
