using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaidouCommon;
using UnityEngine;
using TaidouCommon.Model;
using LitJson;

public class LoginController : ControllerBase
{
    public override OperationCode OpCode
    {
        get
        {
            return OperationCode.Login;
        }
    }

   public  void Login(string usrname,string password)
    {
        User usr = new User() { Username = usrname, Password = password };
        string json = JsonMapper.ToJson(usr);
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.User, json);
        PhotonEngine.Instance.SendRequest(OperationCode.Login, parameters); //登陆的信息被添加到json中传给了服务器
    }

    //相应
    public override void OnOperationResponse(OperationResponse response)
    {
        //Debug.Log("login得到的response code:" + response.ReturnCode);
        //Debug.Log(StartmenuController.username + ":" + StartmenuController.password);
        switch (response.ReturnCode)
        {
            case (short)ReturnCode.Success:
                //根据登陆的用户加载用户的角色信息
                //to do
                MessageManager.instance.ShowMessage(response.DebugMessage, 1);
                StartmenuController.Instance.LoginInGameSelect();
                break;
            case (short)ReturnCode.Fail:
                //
                MessageManager.instance.ShowMessage(response.DebugMessage, 1);
                break;
        }
    }
}
