using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAutoMove : MonoBehaviour {

    public static PlayerAutoMove instance;
    private NavMeshAgent agent;
    public float minDist=2;

    private bool isGetTranscript = false;


    public Transform target;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        agent = this.transform.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (agent.enabled&&isGetTranscript==false)
        {
            if (agent.remainingDistance <= minDist && agent.remainingDistance != 0)
            {
                agent.Stop();
                agent.enabled = false;
                TaskManager.instance.GetDest(); //to taskemanager 找到停下播放任务弹窗
            }
            if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
            {
                agent.Stop();
                agent.enabled = false;
            }

        }else if (agent.enabled && isGetTranscript)
        {
            if (agent.remainingDistance <= minDist && agent.remainingDistance != 0)
            {
                agent.Stop();
                agent.enabled = false;
                FightSceneLod.instance.LoadMap();
                isGetTranscript = false;
            }
            if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
            {
                agent.Stop();
                agent.enabled = false;
                isGetTranscript = false;
            }
        }

        //测试用
        //if (Input.GetMouseButton(0))
        //{
        //    SetDest(target.localPosition);
        //}
	}
    //设置任务目标
    public void SetDest(Vector3 targetPos)
    {
        agent.enabled = true;
        agent.SetDestination(targetPos);
        
    }
    //设置副本入口
    public void SetTranscriptDes(Vector3 targ)
    {
        agent.enabled = true;
        isGetTranscript = true;
        agent.SetDestination(targ);
    }


}
