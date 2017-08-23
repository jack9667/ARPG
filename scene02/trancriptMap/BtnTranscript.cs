using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTranscript : MonoBehaviour {

    public int id;
    public int needLevel;
    public string sceneName;
    public string des = "地下城描述";

	public void OnClick()
    {
        transform.parent.SendMessage("OnBtnTrClick", this); //to transcriptmapui
    }
}
