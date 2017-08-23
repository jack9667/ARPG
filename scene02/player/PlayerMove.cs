using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour {

    public float velocity = 5.0f;
    private NavMeshAgent agent;
    private float h;
    private float v;

    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //float h = Input.GetAxis("Horizontal");  //获得水平轴
        //float v = Input.GetAxis("Vertical");    //获得垂直轴

        h = MyJoyStick.h;
        v = MyJoyStick.v;

        Vector3 vel = this.GetComponent<Rigidbody>().velocity;
        this.GetComponent<Rigidbody>().velocity= new Vector3(-h*velocity, vel.y, -v*velocity);

        //控制朝向
        if (Mathf.Abs(h) > 0.005f || Mathf.Abs(v) > 0.005f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(-h, 0, -v));
        }
        else
        {
            if(agent.enabled==false)
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}
