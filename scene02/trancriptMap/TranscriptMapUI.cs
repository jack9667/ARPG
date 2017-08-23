using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranscriptMapUI : MonoBehaviour {

    public static TranscriptMapUI instance;

    private UIButton btnClose;
    private MapDialog dialog;
    private UIButton enterBtn;

    void Awake()
    {
        instance = this;
        btnClose = transform.Find("btn-close").GetComponent<UIButton>();
        dialog = transform.Find("Dialog").GetComponent<MapDialog>();
        enterBtn = transform.Find("Dialog/btn-enter").GetComponent<UIButton>();

        EventDelegate ed1 = new EventDelegate(this, "OnCloseClick");
        btnClose.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "OnEnter");
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnCloseClick()
    {
        Hide();
    }

    public void OnBtnTrClick(BtnTranscript transcript)  //from btntranscript
    {
        PlayerInfo info = PlayerInfo._instance;
        if (info.LevelNums > transcript.needLevel)
        {
            dialog.ShowDialog(transcript);
        }
        else
        {
            dialog.ShowWarn();
        }
    }

    public void OnEnter()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(2);
        LoadSceneProBar.instance.Show(op);
    }

    
}
