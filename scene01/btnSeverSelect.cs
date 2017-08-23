using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSeverSelect : MonoBehaviour {

    

    void Update()
    {
        
    }

    public void KeepBtnUISprite(GameObject go)
    {
        this.gameObject.GetComponent<UISprite>().spriteName = go.GetComponent<UISprite>().spriteName;
        Debug.Log("fuck");
    }

}
