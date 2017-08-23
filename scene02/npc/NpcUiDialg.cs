using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcUiDialg : MonoBehaviour {

    private UILabel NpcTakeLabel;
    private UIButton accpButton;
    private TweenScale tweenScale;
    public static NpcUiDialg instance;

    void Awake()
    {
        instance = this;

        NpcTakeLabel = transform.Find("Label").GetComponent<UILabel>();
        accpButton = transform.Find("AcceptButton").GetComponent<UIButton>();
        tweenScale = transform.GetComponent<TweenScale>();

        EventDelegate ed1 = new EventDelegate(this, "OnAcceptClick");
        accpButton.onClick.Add(ed1);

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show(string str)
    {
        gameObject.SetActive(true);
        gameObject.transform.localScale = Vector3.zero;
        StartCoroutine(WaitShow(0.3f));
        tweenScale.PlayForward();
        NpcTakeLabel.text = str;
        
    }
    public void Close()
    {
        tweenScale.PlayReverse();
        StartCoroutine(WaitClose(0.3f));
    }
    /// <summary>
    /// to taskemanager.cs
    /// </summary>
    void OnAcceptClick()
    {
        //通知接受任务
        TaskManager.instance.OnAcceptTask();
        //关闭
        Close();
        
    }
    //开始关闭的协程
    IEnumerator WaitClose(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    IEnumerator WaitShow(float time)
    {
        yield return new WaitForSeconds(time);
        
    }
}
