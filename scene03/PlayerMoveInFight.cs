using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveInFight : MonoBehaviour
{

    public float velocity = 5.0f;
    private Animator anim;
    float h;
    float v;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
         //h = Input.GetAxis("Horizontal");  //获得水平轴
         //v = Input.GetAxis("Vertical");    //获得垂直轴

        h = MyJoyStick.h;
        v = MyJoyStick.v;

        Vector3 vel = this.GetComponent<Rigidbody>().velocity;
        
        if (Mathf.Abs(h) > 0.005f || Mathf.Abs(v) > 0.005f)
        {
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("EmptyState"))
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(-h * velocity, vel.y, -v * velocity);
                transform.rotation = Quaternion.LookRotation(new Vector3(-h, 0, -v));//控制朝向
                anim.SetBool("move", true);
            }
            else
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                anim.SetBool("move", false);
            }
            
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            anim.SetBool("move", false);
        }

    }
}
