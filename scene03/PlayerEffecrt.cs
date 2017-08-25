using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffecrt : MonoBehaviour {

    private Renderer[] rendererArray;
    private NcCurveAnimation[] curveArray;

	// Use this for initialization
	void Start () {
        rendererArray = this.GetComponentsInChildren<Renderer>();
        curveArray = this.GetComponentsInChildren<NcCurveAnimation>();
    }
	
	// Update is called once per frame
	void Update () {
        ////for test
        //if (Input.GetMouseButtonDown(0))
        //    Show();
	}

    public void Show()
    {
        foreach(Renderer r in rendererArray)
        {
            r.enabled = true;
        }
        foreach(var c in curveArray)
        {
            c.ResetAnimation();
        }
    }

    
}
