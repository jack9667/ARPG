using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public static PlayerStatus _instance;

    private UISprite headSprite;
    private UILabel levelLabel;
    private UILabel nameLabel;
    private UILabel powerLabel;
    private UISlider expSlider;
    private UILabel expLabel;
    private UILabel diamonLabel;
    private UILabel coinLabel;
    private UILabel energyLabel;
    private UILabel energyRestorePartLabel;
    private UILabel energyRestoreAllLabel;
    private UILabel toughenLabel;
    private UILabel toughenRestorePartLabel;
    private UILabel toughenRestoreAllLabel;
    //status下的动画和按键
    private TweenScale scale;
    private UIButton buttonClose;

    //改名的属性
    private GameObject changeNameGo;
    private UIButton changeNameButton;
    private UIInput nameInput;
    private UIButton sureButton;
    private UIButton colseButton;

    void Awake()
    {
        
        _instance = this;
        scale = this.GetComponent<TweenScale>();
        buttonClose = this.transform.Find("btn-close").GetComponent<UIButton>();

        headSprite = transform.Find("HeadSprite").GetComponent<UISprite>();
        levelLabel = transform.Find("Lv-Label").GetComponent<UILabel>();
        nameLabel = transform.Find("Name-Label").GetComponent<UILabel>();
        powerLabel = transform.Find("Fight-Label/PowerLabel").GetComponent<UILabel>();
        expSlider = transform.Find("ExpProgressBar/Sprite/Sprite").GetComponent<UISlider>();
        expLabel = transform.Find("ExpProgressBar/Label").GetComponent<UILabel>();
        diamonLabel = transform.Find("DiamonSprite/ValueLabel").GetComponent<UILabel>();
        coinLabel = transform.Find("CoinSprite/ValueLabel").GetComponent<UILabel>();
        energyLabel = transform.Find("EnergyLabel/NumberLabel").GetComponent<UILabel>();
        energyRestorePartLabel = transform.Find("EnergyLabel/TimeRecoverLabel").GetComponent<UILabel>();
        energyRestoreAllLabel = transform.Find("EnergyLabel/AllTimeRecoverLabel").GetComponent<UILabel>();
        toughenLabel = transform.Find("ToughenLabel/NumberLabel").GetComponent<UILabel>();
        toughenRestorePartLabel = transform.Find("ToughenLabel/TimeRecoverLabel").GetComponent<UILabel>();
        toughenRestoreAllLabel = transform.Find("ToughenLabel/AllTimeRecoverLabel").GetComponent<UILabel>();


        //将更新人物信息事件添加到委托中
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
        //status关闭按钮事件注册
        EventDelegate ed = new EventDelegate(this,"OnCloseButtonClick");
        buttonClose.onClick.Add(ed);
        //改名字物件初始化
        changeNameButton = transform.Find("btn-ChangeName").GetComponent<UIButton>();
        changeNameGo = transform.Find("ChangeNameSprite").gameObject;
        nameInput = transform.Find("ChangeNameSprite/NameInput").GetComponent<UIInput>();
        sureButton = transform.Find("ChangeNameSprite/SureButton").GetComponent<UIButton>();
        colseButton = transform.Find("ChangeNameSprite/btn-close").GetComponent<UIButton>();

        changeNameButton.onClick.Add(new EventDelegate(this, "OnButtonChangeNameClick"));
        sureButton.onClick.Add(new EventDelegate(this, "OnButtonSureClick"));
        colseButton.onClick.Add(new EventDelegate(this, "OnButtonCancelClick"));
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateEnergyAndToughenShow();
    }

    void OnDestory()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }

    void OnPlayerInfoChanged(PlayerInfo.InfoType type)
    {
        UpdateShow();
    }

    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;
        coinLabel.text = info.CoinNum.ToString();
        diamonLabel.text = info.DiamondNum.ToString();
        headSprite.spriteName = info.HeadPortrait;
        nameLabel.text = info.Name.ToString();
        levelLabel.text = info.LevelNums.ToString();
        powerLabel.text = info.PowerNum.ToString();
        //得到下一等级需要经验
        int requireLvExp = GameController.GetRequireExpByLevel(info.LevelNums);
        expSlider.value =(float) info.ExpNum / requireLvExp;
        expLabel.text = info.ExpNum + "/" + requireLvExp;
        diamonLabel.text = info.DiamondNum.ToString();
        coinLabel.text = info.CoinNum.ToString();

        //UpdateEnergyAndToughenShow();

    }

    //更新体力和历练的显示
    void UpdateEnergyAndToughenShow()
    {
        PlayerInfo info = PlayerInfo._instance;
        //先搞体力
        energyLabel.text = info.EnergyNum + "/100";
        if (info.EnergyNum >= 100)
        {
            energyRestorePartLabel.text = "00:00:00";
            energyRestoreAllLabel.text  = "00:00:00";
        }
        else
        {
            //体力恢复时间
            int remainTime = 60 - (int)info.energyTimer;
            string str = remainTime < 10 ? "0" + remainTime : remainTime.ToString();
            energyRestorePartLabel.text = "00:00:" + str;

            //全部体力回复时间
            int minutes = 99 - info.EnergyNum;
            int hours = minutes / 60;
            minutes = minutes % 60;
            string hoursStr = hours < 10 ? "0" + hours : hours.ToString();
            string minStr = minutes < 10 ? "0" + minutes : minutes.ToString();
            energyRestoreAllLabel.text = hoursStr + ":" + minStr + ":" + str;
        }
        //在搞历练
        toughenLabel.text = info.ToughenNum + "/50";
        if (info.ToughenNum >= 50)
        {
            toughenRestorePartLabel.text = "00:00:00";
            toughenRestoreAllLabel.text = "00:00:00";
        }
        else
        {
            //历练恢复时间
            int remainTime = 60 - (int)info.toughenTimer;
            string str = remainTime < 10 ? "0" + remainTime : remainTime.ToString();
            toughenRestorePartLabel.text = "00:00:" + str;

            //全部历练回复时间
            int minutes = 49 - info.ToughenNum;
            int hours = minutes / 60;
            minutes = minutes % 60;
            string hoursStr = hours < 10 ? "0" + hours : hours.ToString();
            string minStr = minutes < 10 ? "0" + minutes : minutes.ToString();
            toughenRestoreAllLabel.text = hoursStr + ":" + minStr + ":" + str;
        }
    }

    public void Show()
    {
        scale.PlayForward();
    }

    public void OnCloseButtonClick()
    {
        scale.PlayReverse();
    }

    public void OnButtonChangeNameClick()
    {
        changeNameGo.SetActive(true);
    }
    public void OnButtonSureClick()
    {
        //1。首先校验名字是否重复
        //2.成功
        PlayerInfo._instance.ChangeName(nameInput.value);
        if(nameInput.value!="")
        changeNameGo.SetActive(false);
    }
    public void OnButtonCancelClick()
    {
        changeNameGo.SetActive(false);
    }
}
