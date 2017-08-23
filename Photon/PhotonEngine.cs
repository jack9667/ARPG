using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TaidouCommon;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener {

    private static PhotonEngine instance;
    public static PhotonEngine Instance
    {
        get
        {
            return instance;
        }
    }

    private PhotonPeer peer;

    private ConnectionProtocol protocol = ConnectionProtocol.Tcp;
    private string serverAddress = "127.0.0.1:4530";
    private string applicationName = "TaidouServer";

    private bool isConnect = false;

    private Dictionary<byte, ControllerBase> controllers = new Dictionary<byte, ControllerBase>();

    //连接到服务器事件
    public delegate void OnConnectedToServerEvent();
    public event OnConnectedToServerEvent OnConnectedToServer;

    void Awake() {
        instance = this;
        peer = new PhotonPeer(this, protocol);
        peer.Connect(serverAddress, applicationName);
        //peer.Service();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (peer!=null)
            peer.Service();
	}

    

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(level + ":" + message);
    }
    //客户端接受相应
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        ControllerBase cb;
        controllers.TryGetValue(operationResponse.OperationCode, out cb);
        if (cb != null)
        {
            cb.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("fuck OnOperationResponse");
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("OnStatusChanged:" + statusCode);
        switch (statusCode)
        {
            case StatusCode.Connect:
                isConnect = true;
                OnConnectedToServer();
                break;
            default:
                isConnect = false;
                MessageManager.instance.ShowMessage("服务器连接失败", 1);
                break;
        }
    }

    public void OnEvent(EventData eventData)
    {
        throw new NotImplementedException();
    }

    //注册一个控制操作
    public void RegisterController(OperationCode opCode, ControllerBase controller)
    {
        controllers.Add((byte)opCode, controller);
    }
    //销毁
    public void UnRegisterController(OperationCode opCode)
    {
        controllers.Remove((byte)opCode);
    }
    //
    public void SendRequest(OperationCode opCode,Dictionary<byte,object> parameters)
    {
        Debug.Log("给服务端发送的opcode：" + opCode);
        peer.OpCustom((byte)opCode, parameters, true);
    }
}
