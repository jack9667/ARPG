using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerProperty : MonoBehaviour {

    public string ip = "127.0.0.1:9080";
    private string _serverName;
    public int count = 100;
    public string serverName
    {
        get //读 xxx=serverName
        {
            return _serverName;
        }
        set //写 serverName=xxx
        {
            transform.Find("Label").GetComponent<UILabel>().text = value;
            _serverName = value;

        }
    }
    ////把服务器名字更新到当前btn下
    //public void GetserverNameOnLabel(string value)  
    //{
    //    transform.Find("Label").GetComponent<UILabel>().text = value;
    //}


    public void OnPress(bool isPress)
    {
        if (!isPress)
        {
            //选择了当前的服务器
            transform.root.SendMessage("OnServerSelect", this.gameObject);
           
            //transform.Find("btn-sever-selected").SendMessage("KeepBtnUISprite", this.gameObject);

        }
    }
}
