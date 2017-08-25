using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManager : MonoBehaviour {

    public static HpBarManager instance;
    public GameObject hpBatPrefab;
    public GameObject hudTexePrefab;

    void Awake()
    {
        instance = this;
    }

    public GameObject GetHpBar(GameObject tgt)
    {
        GameObject go = NGUITools.AddChild(this.gameObject, hpBatPrefab);
        go.GetComponent<UIFollowTarget>().target = tgt.transform;
        return go;
    }

    public GameObject GetHudText(GameObject tgt)
    {
        GameObject go = NGUITools.AddChild(this.gameObject, hudTexePrefab);
        go.GetComponent<UIFollowTarget>().target = tgt.transform;
        return go;
    }
}
