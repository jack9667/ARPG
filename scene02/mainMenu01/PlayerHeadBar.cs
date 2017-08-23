using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadBar : MonoBehaviour {

    private UISprite headSprite;
    private UILabel nameLabel;
    private UILabel levelLabel;
    private UISlider energySlider;
    private UILabel energySliderLabel;
    private UISlider toughenSlider;
    private UILabel toughenSliderLabel;
    private UIButton energyPlusButton;
    private UIButton toughenPlusButton;

    private UIButton headButton;

    void Awake()
    {
        headSprite = transform.Find("HeadSprite").GetComponent<UISprite>();
        nameLabel= transform.Find("Name-Label").GetComponent<UILabel>();
        levelLabel = transform.Find("Lv-Label").GetComponent<UILabel>();
        energySlider = transform.Find("EnergyProgressBar").GetComponent<UISlider>();
        energySliderLabel = transform.Find("EnergyProgressBar/Label").GetComponent<UILabel>();
        toughenSlider = transform.Find("ToughenProgressBar").GetComponent<UISlider>();
        toughenSliderLabel = transform.Find("ToughenProgressBar/Label").GetComponent<UILabel>();
        energyPlusButton = transform.Find("btn-EnergyPluss").GetComponent<UIButton>();
        toughenPlusButton = transform.Find("btn-ToughenPluss").GetComponent<UIButton>();
        //添加坚监听事件
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;

        headButton = this.transform.Find("HeadButton").GetComponent<UIButton>();
        EventDelegate ed = new EventDelegate(this, "ShowPlayerStatus");
        headButton.onClick.Add(ed);
    }

    void Start()
    {
        
    }

    void OnDestory()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }

    void OnPlayerInfoChanged(PlayerInfo.InfoType type)
    {
        if(type == PlayerInfo.InfoType.All||type == PlayerInfo.InfoType.Name || type == PlayerInfo.InfoType.HeadPortrait || type == PlayerInfo.InfoType.Level || type == PlayerInfo.InfoType.Energy || type == PlayerInfo.InfoType.Toughen)
        {
            UpdateShow();
        }
    }
    //更新状态
    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;
        headSprite.spriteName = info.HeadPortrait;
        nameLabel.text = info.Name.ToString();
        levelLabel.text = info.LevelNums.ToString();
        energySlider.value = info.EnergyNum / 100f;
        energySliderLabel.text = info.EnergyNum + "/100";
        toughenSlider.value = info.ToughenNum / 50f;
        toughenSliderLabel.text = info.ToughenNum + "/50";

    }

    //检测head按钮
   void ShowPlayerStatus()
    {
        PlayerStatus._instance.Show();
    }
}
