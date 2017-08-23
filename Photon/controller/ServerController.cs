using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using TaidouCommon;
using LitJson;

public class ServerController : ControllerBase {
    public override OperationCode OpCode
    {
        get
        {
            return OperationCode.GetServer;
        }
    }

    /// <summary>
    /// 加载服务器列表
    /// </summary>

    public override void Start()
    {
        base.Start();

        PhotonEngine.Instance.OnConnectedToServer += GetServerList;
    }

    public override void OnDestory()
    {
        base.OnDestory();
        PhotonEngine.Instance.OnConnectedToServer -= GetServerList;
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        //接受数据
        Dictionary<byte, object> parameters = response.Parameters;
        object jsonObject = null;
        parameters.TryGetValue((byte)ParameterCode.ServerList, out jsonObject);
        List<TaidouCommon.Model.ServerProperty> serverList = JsonMapper.ToObject<List<TaidouCommon.Model.ServerProperty>>(jsonObject.ToString());


        //int index = 0;
        //ServerProperty spDef=null;
        //GameObject goDef=null;
        foreach (var spTmp in serverList)
        {
            string ip = spTmp.IP + "4530";
            string serverName = spTmp.Name;
            int count = spTmp.Count;
            GameObject go = null;
            if (count > 50)
            {
                go = NGUITools.AddChild(StartmenuController.Instance.serverlistGrid.gameObject, StartmenuController.Instance.serveritemRed);

            }
            else
            {
                go = NGUITools.AddChild(StartmenuController.Instance.serverlistGrid.gameObject, StartmenuController.Instance.serveritemGreen);
            }

            ServerProperty sp = go.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.serverName = serverName;
            sp.count = count;
            StartmenuController.Instance.serverlistGrid.AddChild(go.transform);//对go进行排序
            //if (index == 0)
            //{
            //    spDef = sp;
            //    goDef = go;
            //}
            //index++;
        }
        //StartmenuController.Instance.sp = spDef;
        //StartmenuController.Instance.servernameStart = spDef.name;
    }

    public void GetServerList() {
        PhotonEngine.Instance.SendRequest(TaidouCommon.OperationCode.GetServer, new Dictionary<byte, object>());
    }

 
}
