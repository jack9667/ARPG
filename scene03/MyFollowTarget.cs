using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFollowTarget : MonoBehaviour {

    public GameObject Target;
    public Vector3 offset=new Vector3(0,0,0);
    public float smoothing = 1;

    void FixedUpdate()
    {
        Vector3 tarPos = offset + Target.transform.position;
        transform.position = Vector3.Lerp(transform.position, tarPos, smoothing*Time.deltaTime);
    }
}
