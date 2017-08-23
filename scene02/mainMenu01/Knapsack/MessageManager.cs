using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 提示信息脚本，在MeaagerPanel下
/// </summary>
public class MessageManager : MonoBehaviour {

    public static MessageManager instance;

    private UILabel mesLabel;
    private bool isAcc = false;

    void Awake()
    {
        instance = this;
        mesLabel = transform.Find("Label").GetComponent<UILabel>();
        
    }
    /// <summary>
    /// 显示message
    /// </summary>
    /// <param name="mes"></param>
    /// <param name="time"></param>
    public void ShowMessage(string mes,float time)
    {
            StartCoroutine(Show(mes, time));
    }
    IEnumerator Show(string mes,float t)
    {
        mesLabel.gameObject.SetActive(true);
        //gameObject.transform.localScale = Vector3.one;
        mesLabel.text = mes;
        yield return new WaitForSeconds(t);
        mesLabel.gameObject.SetActive(false);
        //gameObject.transform.localScale = Vector3.zero;
    }
}
