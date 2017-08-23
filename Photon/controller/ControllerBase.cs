using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour {

    public abstract OperationCode OpCode
    {
        get;
    }

    public virtual void Start()
    {
        PhotonEngine.Instance.RegisterController(OpCode, this);
    }

    public abstract void OnOperationResponse(OperationResponse response);

    public virtual void OnDestory()
    {
        PhotonEngine.Instance.UnRegisterController(OpCode);
    }

    
}
