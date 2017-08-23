using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour {

    public static SkillUI instance;

    //private UILabel skill1NameLabel;
    //private UILabel skill2NameLabel;
    //private UILabel skill3NameLabel;

    object[] obj = new object[2];

    private UIButton closeButton;
    private UIButton upgradeButton;
    private UILabel upgradeButtonLabel;
    //private UIPlaySound upPlaysound;

    private Skill skill;    //当前选择的技能

    void Awake()
    {
        instance = this;
        //skill1NameLabel = transform.Find("Skill1Sprite/Label").GetComponent<UILabel>();
        //skill2NameLabel = transform.Find("Skill2Sprite/Label").GetComponent<UILabel>();
        //skill3NameLabel = transform.Find("Skill3Sprite/Label").GetComponent<UILabel>();
        closeButton = transform.Find("btn-close").GetComponent<UIButton>();
        upgradeButton = transform.Find("UpgradeButton").GetComponent<UIButton>();
        upgradeButtonLabel = transform.Find("UpgradeButton/Label").GetComponent<UILabel>();
        //upPlaysound = transform.Find("UpgradeButton").GetComponent<UIPlaySound>();

        //skill1NameLabel.text = "";
        //skill2NameLabel.text = "";
        //skill3NameLabel.text = "";
        DisableUpgradeButton("选择技能");

        EventDelegate ed1 = new EventDelegate(this, "OnUpgrdClick");
        upgradeButton.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "Hide");
        closeButton.onClick.Add(ed2);


    }
    //把升级按钮至为选择技能
    void DisableUpgradeButton(string label = "")
    {
        //把按钮设置不可取
        //upgradeButton.SetState(UIButton.State.Disabled, true);
        ////upgradeButton.GetComponent<Collider>().enabled = false;
        upgradeButton.isEnabled = false;
        //upPlaysound.enabled = false;


        if (upgradeButtonLabel.text != "")
            upgradeButtonLabel.text = label;
    }
    //把升级按钮至为 升级
    void EnableUpdateButton(string label = "")
    {
        //upgradeButton.SetState(UIButton.State.Disabled, false);
        ////upgradeButton.GetComponent<Collider>().enabled = true;
        upgradeButton.isEnabled = true;
        //upPlaysound.enabled = true;
        if (upgradeButtonLabel.text != "")
            upgradeButtonLabel.text = label;
    }
    //from skillitemui 的onclick
    //点击技能触发
    void OnSkillClick(object[] obj)
    {
        this.obj[0] = obj[0];
        this.obj[1] = obj[1];
        PlayerInfo info = PlayerInfo._instance;
        this.skill = obj[0] as Skill;
        UILabel la = obj[1] as UILabel;
        la.text= "当前等级："+skill.Level+'\n'+"攻击力：" + skill.Damage *skill.Level + '\n'+"需要金币：" + skill.Level * 500;
        if (info.CoinNum >= (500 * skill.Level))
        {
            if (skill.Level < info.LevelNums)
            {
                EnableUpdateButton("升级" + skill.Name);
            }
            else
                DisableUpgradeButton("超过最大等级");
        }
        else
        {
            DisableUpgradeButton("金币不足");
        }
        
    }
    //升级按钮
    void OnUpgrdClick()
    {
        PlayerInfo info = PlayerInfo._instance;
        if (skill.Level < info.LevelNums)
        {
            int coinNeed = (500 * skill.Level);
            bool isSuccess = info.GetCoin(coinNeed);
            if (isSuccess)
            {
                skill.Upgrade();
                OnSkillClick(obj);
            }
            else
            {
                DisableUpgradeButton("金币不足");
            }
        }
        else
        {
            DisableUpgradeButton("最大等级");
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
