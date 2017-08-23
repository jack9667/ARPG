using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDialog : MonoBehaviour {

    private UILabel desLabel;
    private UILabel energyTagLabel;
    private UILabel energyLabel;
    private UIButton enterButton;

    void Start()
    {
        desLabel = transform.Find("DesLabel").GetComponent<UILabel>();
        energyTagLabel = transform.Find("EnergyTagLabel").GetComponent<UILabel>();
        energyLabel = transform.Find("EnergyLabel").GetComponent<UILabel>();
        enterButton = transform.Find("btn-enter").GetComponent<UIButton>();
        

        EventDelegate ed1 = new EventDelegate(this, "OnEnter");
        enterButton.onClick.Add(ed1);

        
    }

    public void ShowWarn()
    {
        energyLabel.enabled = false;
        energyTagLabel.enabled = false;
        enterButton.enabled = false;

        desLabel.text = "当前等级无法进入该地下城";
        
    }
    public void ShowDialog(BtnTranscript transcript)
    {
        energyLabel.enabled = true;
        energyTagLabel.enabled = true;
        enterButton.enabled = true;

        desLabel.text = transcript.des;
        energyLabel.text = "3";
        energyTagLabel.text = "需要消耗体力";

    }


    void OnEnter()
    {
        transform.parent.SendMessage("OnEnter");
    }
}
